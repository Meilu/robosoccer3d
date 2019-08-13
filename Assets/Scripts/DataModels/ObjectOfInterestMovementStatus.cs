using System;
using UnityEngine;
using UnityEngine.Events;

namespace DataModels
{
    public class ObjectOfInterestMovementStatus : ObjectOfInterestStatus<ObjectOfInterestMovementStatus>
    {
        #region ObjectMovementSpeed definition
        public event EventHandler ObjectMovementSpeedChangeEvent;
        private ObjectMovementSpeed _objectMovementSpeed;
        public ObjectMovementSpeed ObjectMovementSpeed
        {
            get => _objectMovementSpeed;
            set
            {
                if (value == _objectMovementSpeed)
                    return;

                _objectMovementSpeed = value;
                ObjectMovementSpeedChangeEvent?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
        
        #region ObjectMovementDirection definition
        public event EventHandler ObjectMovementDirectionChangeEvent;
        private ObjectMovementDirection _objectMovementDirection;
        public ObjectMovementDirection ObjectMovementDirection
        {
            get => _objectMovementDirection;
            set
            {
                if (value == _objectMovementDirection)
                    return;
                
                _objectMovementDirection = value;
                ObjectMovementDirectionChangeEvent?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
        
        public override ObjectOfInterestMovementStatus Copy()
        {
            return new ObjectOfInterestMovementStatus()
            {
                ObjectName = ObjectName,
                ObjectMovementSpeed = ObjectMovementSpeed,
                GameObjectToFind = GameObjectToFind
            };
        }
    }
}