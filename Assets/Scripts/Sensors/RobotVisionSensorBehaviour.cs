﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataModels;
using UnityEngine;
using UnityEngine.Events;

namespace Sensors
{
    public class  RobotVisionSensorBehaviour: RobotSensorBehaviour<ObjectOfInterestVisionStatus>
    {
        public float viewAngle;

        private RobotVisionSensor RobotVisionSensor { get; set; }
        private List<ObjectOfInterestVisionStatus> _objectOfInterestVisionStatusses;
        RobotVisionSensorBehaviour()
        {
            RobotVisionSensor = new RobotVisionSensor(viewAngle);
        }
        
        private void Start()
        {
            var tempObjectOfInterestStatusses = new List<ObjectOfInterestVisionStatus>() {
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.SoccerBallObjectName,
                    MinimunDistance = 0.3f
                },
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.AwayGoalLine,
                    MinimunDistance = 3.0f
                },
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.HomeGoalLine
                }
            };

            foreach (var objectOfInterestVisionStatus in tempObjectOfInterestStatusses)
            {   
                objectOfInterestVisionStatus.GameObjectToFind = GameObject.Find(objectOfInterestVisionStatus.ObjectName);
            }
            
            // Find all other robots by their tag name.
            var otherRobotsByTag = RobotVisionSensor.CreateObjectOfInterestForGameObjects(GameObject.FindGameObjectsWithTag(Settings.OtherRobotsTagName));

            // Merge lists together.
            _objectOfInterestVisionStatusses = tempObjectOfInterestStatusses.Union(otherRobotsByTag).ToList();
        }
        
        /// <summary>
        /// Will continuously check all properties of the objects of interest we are interested in and update their status.
        /// </summary>
        protected override void UpdateObjectsOfInterestStatuses()
        {
            foreach (var objectOfInterestVisionStatus in _objectOfInterestVisionStatusses)
            {
                // If object not found, don't continue this iteration but go to the next.
                if (!objectOfInterestVisionStatus.GameObjectToFind)
                    continue;
  
                // Update the distance and vision angle status of this object of interest.
                objectOfInterestVisionStatus.IsWithinDistance = RobotVisionSensor.IsObjectWithinDistance(objectOfInterestVisionStatus.GameObjectToFind.transform.position, transform.position, objectOfInterestVisionStatus.MinimunDistance);
                objectOfInterestVisionStatus.IsInsideVisionAngle = IsObjectInsideVisionAngle(objectOfInterestVisionStatus.GameObjectToFind.transform.position, objectOfInterestVisionStatus.ObjectName, transform.position, transform.up, transform.forward);
            }
        }

        private bool IsObjectInsideVisionAngle(Vector3 objectToFindPosition, string objectToFindName, Vector3 fromPosition, Vector3 upPointingVector, Vector3 forwardPointingVector)
        {
            if (RobotVisionSensor.IsObjectInsideVisionAngle(objectToFindPosition, fromPosition, forwardPointingVector))
                return true;

            if (!RobotVisionSensor.IsObjectWithinDistance(objectToFindPosition, fromPosition, 1.0f))
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
        public List<ObjectOfInterestVisionStatus> ObjectOfInterestVisionStatuses;
        private float _viewAngle;

        public RobotVisionSensor(float viewAngle)
        {
            _viewAngle = viewAngle;
        }
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
        public bool IsObjectWithinDistance(Vector3 objectToFindPosition, Vector3 robotObjectPosition, float maxDistanceToCheck)
        {
            // Check whether the object is within the given max distance.
            return Vector3.Distance(objectToFindPosition, robotObjectPosition) < maxDistanceToCheck;
        }

        /// <summary>
        /// Checks whether the object is inside the robot's vision angle or not
        /// </summary>
        /// <param name="objectToFind"></param>
        /// <returns>bool</returns>
        public bool IsObjectInsideVisionAngle(Vector3 objectToFindPosition, Vector3 fromPosition, Vector3 forwardPointingVector)
        {
            // Get the direction to the object
            var directionToObject = (objectToFindPosition - fromPosition);

            // Calculate the angle to the object and check if it's inside our viewAngle.
            return Vector3.Angle(forwardPointingVector, directionToObject) < _viewAngle;
        }
    }
}
