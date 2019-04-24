using Sensors;
using UnityEngine;

namespace Planners
{
    public class RobotVisionPlanner : MonoBehaviour
    {
        private RobotVisionActuator _robotVisionActuator;

        private bool HasBall = false;

        public bool BallInsideVision = false;
        public bool BallCloseVision = false;
        public bool GoalInsideVision = false;
        public bool GoalCloseVision = false;

        //every event that will be listened to should be here
        void Start()
        {
            // Get the vision component
            var fovComponent = transform.Find("fov").GetComponent<RobotVisionSensor>();

            // Bind our function to the unity event of the fov component, which will trigger when the ball is or is not inside this robot's vision.
            fovComponent.ballInsideVision.AddListener(SetBallInsideVision);
            fovComponent.ballNotInsideVision.AddListener(SetBallOutsideVision);
            fovComponent.ballInsideShootingRange.AddListener(SetBallCloseVision);
            fovComponent.ballNotInsideShootingRange.AddListener(SetBallNotCloseVision);
            fovComponent.goalInsideVision.AddListener(SetGoalInsideVision);
            fovComponent.goalNotInsideVision.AddListener(SetGoalOutsideVision);

            _robotVisionActuator = transform.GetComponent<RobotVisionActuator>();

        }

        private void SetBallInsideVision() => BallInsideVision = true;
        private void SetBallOutsideVision() => BallInsideVision = false;
        private void SetBallCloseVision() => BallCloseVision = true;
        private void SetBallNotCloseVision() => BallCloseVision = false;
        private void SetGoalInsideVision() => GoalInsideVision = true;
        private void SetGoalOutsideVision() => GoalInsideVision = false;
        //TO DO: private void SetGoalCloseVision() => GoalCloseVision = true;
        //TO DO: private void SetGoalNotCloseVision() => GoalCloseVision = false;

        void FixedUpdate()
        {

//            // if ball is inside vision then do things
            if (BallInsideVision == true)
            {
                _robotVisionActuator.TriggerAction(RobotMotorAction.MoveForward);
                _robotVisionActuator.TriggerAction(RobotWheelAction.DoNothing);
            }
//
            //if ball is not inside vision then do other things
            if (BallInsideVision == false)
            {
                _robotVisionActuator.TriggerAction(RobotWheelAction.TurnLeft);
                _robotVisionActuator.TriggerAction(RobotMotorAction.DoNothing);
            }

            //Try to find goal
            if (GoalInsideVision == false && BallInsideVision == true)
            {
                _robotVisionActuator.TriggerAction(RobotWheelAction.TurnRight);
                _robotVisionActuator.TriggerAction(RobotMotorAction.DoNothing);
                _robotVisionActuator.TriggerAction(RobotLegAction.DoNothing);
            }

            //Try to find goal
            if (GoalInsideVision == false)
            {
                _robotVisionActuator.TriggerAction(RobotLegAction.DoNothing);
            }

            //Try to find goal
            if (GoalInsideVision == true && BallCloseVision == true)
            {
                _robotVisionActuator.TriggerAction(RobotLegAction.KickForward);
                _robotVisionActuator.TriggerAction(RobotMotorAction.DoNothing);
            }

            //Try to score
            if (GoalCloseVision == true && BallCloseVision == true)
            {
                _robotVisionActuator.TriggerAction(RobotLegAction.KickForward);
                _robotVisionActuator.TriggerAction(RobotMotorAction.DoNothing);
                _robotVisionActuator.TriggerAction(RobotWheelAction.DoNothing);
            }

        }
    }
}
