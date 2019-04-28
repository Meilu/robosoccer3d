using System;
using UnityEngine;
using UnityEngine.Events;

namespace DataModels
{
    public class ObjectOfInterestVisionStatus
    {
        public string ObjectName;
        public string RobotName;

        #region IsInsideVision definition
        public event EventHandler IsInsideVisionAngleChangeEvent;
        private bool _isInsideVisionAngle;

        public bool IsInsideVisionAngle
        {
            get => _isInsideVisionAngle;
            set
            {
                if (value == _isInsideVisionAngle)
                    return;

                _isInsideVisionAngle = value;
                IsInsideVisionAngleChangeEvent?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
        
        #region IsWithinDistance definition
        public event EventHandler IsWithinDistanceChangeEvent;
        private bool _isWithinDistance;
        public bool IsWithinDistance
        {
            get => _isWithinDistance;
            set
            {
                if (value == _isWithinDistance)
                    return;
                
                _isWithinDistance = value;
                IsWithinDistanceChangeEvent?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
        public GameObject GameObjectToFind;
    }
}