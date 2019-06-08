using System.Collections.Generic;
using DataModels;
using UnityEngine;

namespace Sensors
{
    public abstract class RobotSensorBehaviour<T> : MonoBehaviour
    {
        protected abstract RobotSensor<T> RobotSensor { get; set; }

        private void FixedUpdate()
        {
            RobotSensor.UpdateObjectsOfInterestStatuses();
        }
    }

    public abstract class RobotSensor<T>
    {
        [HideInInspector] public List<T> ObjectsOfInterestStatus;

        public abstract void UpdateObjectsOfInterestStatuses();
    }
}