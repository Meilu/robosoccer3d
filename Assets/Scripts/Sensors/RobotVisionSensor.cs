﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataModels;
using UnityEngine;
using UnityEngine.Events;

namespace Sensors
{
    public class RobotVisionSensor : MonoBehaviour
    {
        [Range(0,360)]
        public float viewAngle;

        [HideInInspector] public IList<ObjectOfInterestVisionStatus> objectsOfInterestVisionStatus;
        RobotVisionSensor()
        {
            // Initialize the list of items this robot is interested in with his visionsensor.
            // For now these are hardcoded, but in the future we may want to pass these dynamically because each robot may be interested in different objects.
            objectsOfInterestVisionStatus = new List<ObjectOfInterestVisionStatus>() {         
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.SoccerBallObjectName
                }
            };
        }
        private void FixedUpdate()
        {
            FindVisibleTargets(); 
        }

        /// <summary>
        /// Will loop through all objects of interest and update their status if needed.
        /// </summary>
        private void FindVisibleTargets()
        {
            foreach (var objectOfInterestVisionStatus in objectsOfInterestVisionStatus)
            {
                var gameObjectToFind = GameObject.Find(objectOfInterestVisionStatus.ObjectName);

                // If object not found, don't continue this iteration but go to the next.
                if (!gameObjectToFind)
                    continue;
                
                objectOfInterestVisionStatus.IsWithinDistance = IsObjectWithinDistance(gameObjectToFind, 1.0f);
                objectOfInterestVisionStatus.IsInsideVisionAngle = IsObjectInsideVisionAngle(gameObjectToFind);   
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
            return Vector3.Angle(currentTransform.forward, directionToObject) < viewAngle / 2;
        }

        /// <summary>
        /// Checks whether the object is inside the given max distance.
        /// </summary>
        /// <param name="objectToFind">The object </param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        private bool IsObjectWithinDistance(GameObject objectToFind, float maxDistance)
        {
            if (!objectToFind)
                return false;
            
            // Get current position of the robot.
            var robotObjectPosition = transform.parent.position;
            
            // Check whether the object is within the given max distance.
            return Vector3.Distance(objectToFind.transform.position, robotObjectPosition) < maxDistance;
        }
    }
}
