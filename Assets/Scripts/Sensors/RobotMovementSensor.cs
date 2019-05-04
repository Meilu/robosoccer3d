using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataModels;
using UnityEngine;
using UnityEngine.Events;

namespace Sensors
{
    public class RobotMovementSensor : RobotSensor<ObjectOfInterestMovementStatus>
    {

        private void Start()
        {
            objectsOfInterestStatus = new List<ObjectOfInterestMovementStatus>() {
                new ObjectOfInterestMovementStatus()
                {
                    ObjectName = transform.parent.name
                }
            };
            
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
            foreach (var objectOfInterestMovementStatus in objectsOfInterestStatus)
            {
                // If object not found, don't continue this iteration but go to the next.
                if (!objectOfInterestMovementStatus.GameObjectToFind)
                    continue;

                objectOfInterestMovementStatus.ObjectMovementSpeed = GetObjectSpeed(objectOfInterestMovementStatus.GameObjectToFind);
            }
        }

        private ObjectMovementSpeed GetObjectSpeed(GameObject gameObjectToFind)
        {
            if (!gameObjectToFind)
                return ObjectMovementSpeed.None;

            var rigidBody = gameObjectToFind.GetComponent<Rigidbody>();

            if (!rigidBody)
                return ObjectMovementSpeed.None;

            switch (rigidBody.velocity.magnitude)
            {
                case 0:
                    return ObjectMovementSpeed.Still;
                case 1:
                    return ObjectMovementSpeed.Slow;
                case 2:
                    return ObjectMovementSpeed.Medium;
                case 3:
                    return ObjectMovementSpeed.LightSpeed;
            }

            return ObjectMovementSpeed.None;
        }
    }
}
