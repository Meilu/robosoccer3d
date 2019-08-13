using UnityEngine;

namespace EventSystem.Events
{
    public class BallCrossedLineEvent : BaseEvent
    {
        public Vector3 Position;
        public FieldBoundType FieldBoundType;
        
        public BallCrossedLineEvent(Vector3 position, FieldBoundType ballBoundType)
        {
            Position = position;
            FieldBoundType = ballBoundType;
        }
    }
}