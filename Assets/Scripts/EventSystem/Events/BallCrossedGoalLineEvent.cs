using UnityEngine;

namespace EventSystem.Events
{
    public class BallCrossedGoalLineEvent : BaseEvent
    {
        public Vector3 Position;
        public BallBoundType BallBoundType;
        
        public BallCrossedGoalLineEvent(Vector3 position, BallBoundType ballBoundType)
        {
            Position = position;
            BallBoundType = ballBoundType;
        }
    }
}