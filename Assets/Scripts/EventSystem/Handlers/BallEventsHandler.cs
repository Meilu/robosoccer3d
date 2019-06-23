using EventSystem.Events;

namespace EventSystem.Handlers
{
    public class BallEventsHandler : EventHandler
    {
        public override void SubscribeEvents()
        {
            EventManager.Instance.AddListener<BallCrossedGoalLineEvent>(OnBallCrossedGoalLine);
        }

        public override void UnsubscribeEvents()
        {
        }

        public void OnBallCrossedGoalLine(BallCrossedGoalLineEvent e)
        {
            print("ball crosed the line from handler");
            print(e.Position);
            print(e.BallBoundType);
        }
    }
}