using System.Collections;
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
        [UnityEngine.Range(0,360)]
        public float viewAngle;

        RobotVisionSensorBehaviour()
        {
            RobotSensor = new RobotVisionSensor();
            // Initialize the list of items this robot is interested in with his visionsensor.
            // For now these are hardcoded, but in the future we may want to pass these dynamically because each robot may be interested in different objects.
            objectsOfInterestStatus = new List<ObjectOfInterestVisionStatus>() {
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
        }
        
        private void Start()
        {
            foreach (var objectOfInterestVisionStatus in objectsOfInterestStatus)
            {   
                objectOfInterestVisionStatus.GameObjectToFind = GameObject.Find(objectOfInterestVisionStatus.ObjectName);
            }
            
            // Find all other robots by their tag name.
            var otherRobotsByTag = GetObjectsOfInterestByTag(Settings.OtherRobotsTagName);
            
            // Merge them with our existing list so that their vision status will be updated.
            objectsOfInterestStatus = objectsOfInterestStatus.Union(otherRobotsByTag).ToList();
        }

        private List<ObjectOfInterestVisionStatus> GetObjectsOfInterestByTag(string tag)
        {
            // Get a list of all gameobjects on the field with a specific tag
            var gameObjectsByTag = GameObject.FindGameObjectsWithTag(tag);

            var objectsOfInterestByTag = new List<ObjectOfInterestVisionStatus>();
            
            foreach (var gameObjectByTag in gameObjectsByTag)
            {
                // Make sure we cannot add ourselves
                if (gameObjectByTag.Equals(transform.parent.gameObject))
                    continue;
                
                objectsOfInterestByTag.Add(new ObjectOfInterestVisionStatus()
                {
                    GameObjectToFind = gameObjectByTag
                });
            }

            return objectsOfInterestByTag;
        }
        
        /// <summary>
        /// Will continuously check all properties of the objects of interest we are interested in and update their status.
        /// </summary>
        protected override void UpdateObjectsOfInterestStatuses()
        {
            foreach (var objectOfInterestVisionStatus in objectsOfInterestStatus)
            {
                // If object not found, don't continue this iteration but go to the next.
                if (!objectOfInterestVisionStatus.GameObjectToFind)
                    continue;
  
                // Update the distance and vision angle status of this object of interest.
              objectOfInterestVisionStatus.IsWithinDistance = IsObjectWithinDistance(objectOfInterestVisionStatus.GameObjectToFind, objectOfInterestVisionStatus.MinimunDistance);
              objectOfInterestVisionStatus.IsInsideVisionAngle = IsObjectInsideVisionAngle(objectOfInterestVisionStatus.GameObjectToFind);   
            }
        }

        /// <summary>
        /// Checks whether the object is inside the robot's vision angle or not
        /// </summary>
        /// <param name="objectToFind"></param>
        /// <returns>bool</returns>
        private bool IsObjectInsideVisionAngle(GameObject objectToFind)
        {
            if (!objectToFind)
                return false;

            var currentTransform = transform;
            
            // Get current position of the robot.
            var robotObjectPosition = currentTransform.parent.position;
            
            // Get the direction to the object
            var directionToObject = (objectToFind.transform.position - robotObjectPosition);
            
            // Calculate the angle to the object and check if it's inside our viewAngle.
            var isInAngle = Vector3.Angle(currentTransform.forward, directionToObject) < viewAngle;

            // If the object is already inside of our viewangle, just return immediately.
            if (isInAngle)
                return true;

            if (!IsObjectWithinDistance(objectToFind, 1.0f))
                return false;

            if (!IsRobotFacingObjectToFind(objectToFind, robotObjectPosition, currentTransform))
                return false;
            
            // Some object can be quite large and the geometric center will not be inside our angle, even though a part of the object could still be in our vision.
            // We can double check with raycasts if this is the case, but only if we are facing this object and the object is close to us.
            return IsObjectInsideRaycastAngle(objectToFind, robotObjectPosition, currentTransform);
        }

        private bool IsObjectInsideRaycastAngle(GameObject objectToFind, Vector3 robotObjectPosition, Transform currentTransform)
        {
            Vector3 rayPosition = new Vector3(robotObjectPosition.x, currentTransform.position.y, robotObjectPosition.z);
            Vector3 leftRayRotation = Quaternion.AngleAxis(-viewAngle, currentTransform.up) * currentTransform.forward;
            Vector3 rightRayRotation = Quaternion.AngleAxis(viewAngle, currentTransform.up) * currentTransform.forward;
            
            var rays = new List<Ray>()
            {
                new Ray(rayPosition, transform.forward),
                new Ray(rayPosition, leftRayRotation),
                new Ray(rayPosition, rightRayRotation)
            };

            foreach (var ray in rays)
            {
                if (!Physics.Raycast(ray, out var hit))
                    continue;
                
                // If this collider hit the object we were looking for
                if (hit.collider.name != objectToFind.name)
                    continue;
                
                return true;
            }

            return false;
        }

        private bool IsRobotFacingObjectToFind(GameObject objectToFind, Vector3 robotObjectPosition, Transform currentTransform)
        {
            Vector3 rayPosition = new Vector3(robotObjectPosition.x, currentTransform.position.y, robotObjectPosition.z);
            var forwardRay = new Ray(rayPosition, transform.forward);
            
            if (Physics.Raycast(forwardRay, out var hit))
                return hit.collider.name == objectToFind.name;
            
            return false;
        }

        /// <summary>
        /// Checks whether the object is inside the given max distance.
        /// </summary>
        /// <param name="objectToFind">The object </param>
        /// <param name="maxDistanceToCheck"></param>
        /// <returns></returns>
        private bool IsObjectWithinDistance(GameObject objectToFind, float maxDistanceToCheck)
        {
            if (!objectToFind)
                return false;
            
            // Get current position of the robot.
            var robotObjectPosition = transform.parent.position;
            
            // Check whether the object is within the given max distance.
            return Vector3.Distance(objectToFind.transform.position, robotObjectPosition) < maxDistanceToCheck;
        }
    }

    public class RobotVisionSensor : RobotSensor<ObjectOfInterestVisionStatus>
    {
        public override void UpdateObjectsOfInterestStatuses()
        {
            throw new System.NotImplementedException();
        }
    }

}
