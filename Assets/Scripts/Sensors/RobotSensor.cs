using System.Collections.Generic;
using DataModels;
using UnityEngine;

namespace Sensors
{
    public abstract class RobotSensor<T> : MonoBehaviour
    {
        protected abstract void UpdateObjectsOfInterestStatuses();
        [HideInInspector] public List<T> objectsOfInterestStatus;
        
        private void FixedUpdate()
        {
            // Constantly check the statuses of the object of interest list.
            UpdateObjectsOfInterestStatuses(); 
        }
    }
}