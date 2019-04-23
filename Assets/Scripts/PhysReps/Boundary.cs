using UnityEngine;

namespace PhysReps
{
    public class Boundary : MonoBehaviour
    {
        public BoundarySide side;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Only check the ball.
            if (collision.name != "soccerball")
                return;

            switch (side)
            {
                case BoundarySide.LeftGoalLine:
                    EventManager.Instance.TriggerEvent(EventType.HomeGoal);
                    break;
                case BoundarySide.RightGoalLine:
                    EventManager.Instance.TriggerEvent(EventType.AwayGoal);
                    break;
                default:
                    EventManager.Instance.TriggerEvent(EventType.BallOut);
                    break;
                
            }
            
        }

     
    }
}
