using EventSystem.Events;

namespace EventSystem.Handlers
{
    public class BallEventsHandler : EventHandler
    {
        public override void SubscribeEvents()
        {
            EventManager.Instance.AddListener<BallCrossedLineEvent>(OnBallCrossedLine);
        }

        public override void UnsubscribeEvents()
        {
        }

        public void OnBallCrossedLine(BallCrossedLineEvent e)
        {
            print("ball crosed the line from handler");
            print(e.Position);
            print(e.FieldBoundType);
        }
    }
}