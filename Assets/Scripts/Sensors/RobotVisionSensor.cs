﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataModels;
using UnityEngine;
using UnityEngine.Events;

namespace Sensors
{
    public class RobotVisionSensor : RobotSensor<ObjectOfInterestVisionStatus>
    {
        [Range(0,360)]
        public float viewAngle;
        public float maxDistance = 0.5f;

        RobotVisionSensor()
        {
            // Initialize the list of items this robot is interested in with his visionsensor.
            // For now these are hardcoded, but in the future we may want to pass these dynamically because each robot may be interested in different objects.
            objectsOfInterestStatus = new List<ObjectOfInterestVisionStatus>() {
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.SoccerBallObjectName
                },
                // Add the other robots as a different object to the list :) so the robot can have their own status changes.
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.OtherRobots
                },
                new ObjectOfInterestVisionStatus()
                {
                    ObjectName = Settings.AwayGoalLine
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
                objectOfInterestVisionStatus.IsWithinDistance = IsObjectWithinDistance(objectOfInterestVisionStatus.GameObjectToFind, maxDistance);
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
            return Vector3.Angle(currentTransform.forward, directionToObject) < viewAngle / 2;
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
}
