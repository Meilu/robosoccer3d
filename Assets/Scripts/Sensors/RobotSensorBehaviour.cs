using System.Collections.Generic;
using DataModels;
using UnityEngine;

namespace Sensors
{
    public abstract class RobotSensorBehaviour<T> : MonoBehaviour
    {
        protected abstract void UpdateObjectsOfInterestStatuses();

        private void FixedUpdate()
        {
            UpdateObjectsOfInterestStatuses();
        }
    }

    public abstract class RobotSensor
    {
        
    }
}