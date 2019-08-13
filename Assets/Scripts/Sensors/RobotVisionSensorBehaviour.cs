using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataModels;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Sensors
{
    public class  RobotVisionSensorBehaviour: RobotSensorBehaviour<ObjectOfInterestVisionStatus>
    {
        public float viewAngle;

        private RobotVisionSensor _robotVisionSensor { get; }
        public List<ObjectOfInterestVisionStatus> objectOfInterestVisionStatuses;
        RobotVisionSensorBehaviour()
        {
            _robotVisionSensor = new RobotVisionSensor();
        }
        
        private void Awake()
        {
            var tempObjectOfInterestStatuses = new List<ObjectOfInterestVisionStatus>() {
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.SoccerBallObjectName,
                    MaxReachDistance = 2.0f
                },
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.AwayGoalLine,
                    MaxReachDistance = 3.0f
                },
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.HomeGoalLine
                }
            };
            
            foreach (var objectOfInterestVisionStatus in tempObjectOfInterestStatuses)
            {   
                objectOfInterestVisionStatus.GameObjectToFind = GameObject.Find(objectOfInterestVisionStatus.ObjectName);
            }
            
            // Find all other robots by their tag name.
            var otherRobotsByTag = _robotVisionSensor.CreateObjectOfInterestForGameObjects(GameObject.FindGameObjectsWithTag(Settings.OtherRobotsTagName));

            // Merge both temporary lists together.
            objectOfInterestVisionStatuses = tempObjectOfInterestStatuses.Union(otherRobotsByTag).ToList();
        }
        
        /// <summary>
        /// Will continuously check all properties of the objects of interest we are interested in and update their status.
        /// </summary>
        protected override void UpdateObjectsOfInterestStatuses()
        {
            foreach (var objectOfInterestVisionStatus in objectOfInterestVisionStatuses)
            {
                // If object not found, don't continue this iteration but go to the next.
                if (!objectOfInterestVisionStatus.GameObjectToFind)
                    continue;

                var objectPosition = objectOfInterestVisionStatus.GameObjectToFind.transform.position;
                var position = transform.position;
                var distanceFromObject = _robotVisionSensor.DistanceFromObject(objectPosition, position);
                
                // Update properties of interest that belong to this object.
                objectOfInterestVisionStatus.DistanceFromObject = distanceFromObject;
                objectOfInterestVisionStatus.IsWithinDistance = _robotVisionSensor.IsObjectWithinDistance(objectPosition, position, objectOfInterestVisionStatus.MaxReachDistance, distanceFromObject);
                objectOfInterestVisionStatus.IsInsideVisionAngle = IsObjectInsideVisionAngle(objectPosition, objectOfInterestVisionStatus.ObjectName, position, transform.up, transform.forward, distanceFromObject);
            }
        }

        private bool IsObjectInsideVisionAngle(Vector3 objectToFindPosition, string objectToFindName, Vector3 fromPosition, Vector3 upPointingVector, Vector3 forwardPointingVector, float distanceFromObject)
        {
            // Not even going to bother with unit testing these. Unit testing raycasts is a bitch.
            if (_robotVisionSensor.IsObjectInsideVisionAngle(objectToFindPosition, fromPosition, forwardPointingVector, viewAngle))
                return true;

            if (!_robotVisionSensor.IsObjectWithinDistance(objectToFindPosition, fromPosition, 1.0f, distanceFromObject))
                return false;

            if (!IsRobotFacingObjectToFind(objectToFindName, fromPosition, forwardPointingVector))
                return false;

            // Some object can be quite large and the geometric center will not be inside our angle, even though a part of the object could still be in our vision.
            // We can double check with raycasts if this is the case, but only if we are facing this object and the object is close to us.
            return IsObjectInsideRaycastAngle(objectToFindName, fromPosition, upPointingVector, forwardPointingVector);
        }
        private bool IsRobotFacingObjectToFind(string objectToFindName, Vector3 fromPosition, Vector3 forwardPointingVector)
        {
            var forwardRay = new Ray(fromPosition, forwardPointingVector);

            if (Physics.Raycast(forwardRay, out var hit))
                return hit.collider.name == objectToFindName;

            return false;
        }

        private bool IsObjectInsideRaycastAngle(string objectToFindName, Vector3 fromPosition, Vector3 upPointingVector, Vector3 forwardPointingVector)
        {
            Vector3 leftRayRotation = Quaternion.AngleAxis(-viewAngle, upPointingVector) * forwardPointingVector;
            Vector3 rightRayRotation = Quaternion.AngleAxis(viewAngle, upPointingVector) * forwardPointingVector;
            
            var rays = new List<Ray>()
            {
                new Ray(fromPosition, transform.forward),
                new Ray(fromPosition, leftRayRotation),
                new Ray(fromPosition, rightRayRotation)
            };

            foreach (var ray in rays)
            {
                if (!Physics.Raycast(ray, out var hit))
                    continue;
                
                // If this collider hit the object we were looking for
                if (hit.collider.name != objectToFindName)
                    continue;
                
                return true;
            }

            return false;
        }
    }

    public class RobotVisionSensor : RobotSensor
    {
        public IEnumerable<ObjectOfInterestVisionStatus> CreateObjectOfInterestForGameObjects(GameObject[] gameObjects)
        {
            foreach (var gameObjectByTag in gameObjects)
            {
                yield return new ObjectOfInterestVisionStatus()
                {
                    GameObjectToFind = gameObjectByTag
                };
            }
        }
        /// <summary>
        /// Checks whether the object is inside the given max distance.
        /// </summary>
        public bool IsObjectWithinDistance(Vector3 objectToFindPosition, Vector3 robotObjectPosition, float maxDistanceToCheck, float distance)
        {
            // Check whether the object is within the given max distance.
            return distance < maxDistanceToCheck;
        }

        public float DistanceFromObject(Vector3 objectToFindPosition, Vector3 robotObjectPosition)
        {
            return Vector3.Distance(objectToFindPosition, robotObjectPosition);

        }

        /// <summary>
        /// Checks whether the object is inside the robot's vision angle or not
        /// </summary>
        public bool IsObjectInsideVisionAngle(Vector3 objectToFindPosition, Vector3 fromPosition, Vector3 forwardPointingVector, float viewAngle)
        {
            // Get the direction to the object
            var directionToObject = objectToFindPosition - fromPosition;

            // Calculate the angle to the object and check if it's inside our viewAngle.
            return Vector3.Angle(forwardPointingVector, directionToObject) < viewAngle;
        }
    }
}
