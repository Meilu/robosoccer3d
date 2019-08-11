using UnityEngine;

namespace EventSystem.Events
{
    public class BallCrossedLineEvent : BaseEvent
    {
        public Vector3 Position;
        public BallBoundType BallBoundType;
        
        public BallCrossedLineEvent(Vector3 position, BallBoundType ballBoundType)
        {
            Position = position;
            BallBoundType = ballBoundType;
        }
    }
}