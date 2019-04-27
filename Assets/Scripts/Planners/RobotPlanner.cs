using System.Collections.Specialized;
using System.Linq;
using Actuators;
using Sensors;
using UnityEngine;

namespace Planners
{
    public class RobotPlanner : MonoBehaviour
    {
        private RobotActuator _robotVisionActuator;
        private RobotVisionSensor _robotVisionSensor;
        
       
        void Start()
        {
            _robotVisionActuator = transform.GetComponent<RobotActuator>();
            _robotVisionSensor = transform.Find(Settings.RobotFieldOfViewObjectName).GetComponent<RobotVisionSensor>();
        }
        
        private void FixedUpdate()
        {
            var ballObjectStatus = _robotVisionSensor.objectsOfInterestVisionStatus.FirstOrDefault(x => x.ObjectName == Settings.SoccerBallObjectName);
            
            if (ballObjectStatus == null)
                return;

            if (ballObjectStatus.IsInsideVisionAngle)
                TriggerMoveForward();
        }
        
        private void TriggerMoveForward()
        {
            _robotVisionActuator.TriggerAction(RobotMotorAction.MoveForward);
        }

        private void TriggerTurnLeft()
        {
            _robotVisionActuator.TriggerAction(RobotWheelAction.TurnLeft);
        }
        
        private void TriggerTurnRight()
        {
            _robotVisionActuator.TriggerAction(RobotWheelAction.TurnLeft);
        }

        private void TriggerKickForward()
        {
            _robotVisionActuator.TriggerAction(RobotLegAction.KickForward);
        }

        


////            // if ball is inside vision then do things
//            if (BallInsideVision == true)
//            {
//                _robotVisionActuator.TriggerAction(RobotMotorAction.MoveForward);
//                _robotVisionActuator.TriggerAction(RobotWheelAction.DoNothing);
//            }
////
//            //if ball is not inside vision then do other things
//            if (BallInsideVision == false)
//            {
//                _robotVisionActuator.TriggerAction(RobotWheelAction.TurnLeft);
//                _robotVisionActuator.TriggerAction(RobotMotorAction.DoNothing);
//            }
//
//            //Try to find goal
//            if (GoalInsideVision == false && BallInsideVision == true)
//            {
//                _robotVisionActuator.TriggerAction(RobotWheelAction.TurnRight);
//                _robotVisionActuator.TriggerAction(RobotMotorAction.DoNothing);
//                _robotVisionActuator.TriggerAction(RobotLegAction.DoNothing);
//            }
//
//            //Try to find goal
//            if (GoalInsideVision == false)
//            {
//                _robotVisionActuator.TriggerAction(RobotLegAction.DoNothing);
//            }
//
//            //Try to find goal
//            if (GoalInsideVision == true && BallCloseVision == true)
//            {
//                _robotVisionActuator.TriggerAction(RobotLegAction.KickForward);
//                _robotVisionActuator.TriggerAction(RobotMotorAction.DoNothing);
//            }
//
//            //Try to score
//            if (GoalCloseVision == true && BallCloseVision == true)
//            {
//                _robotVisionActuator.TriggerAction(RobotLegAction.KickForward);
//                _robotVisionActuator.TriggerAction(RobotMotorAction.DoNothing);
//                _robotVisionActuator.TriggerAction(RobotWheelAction.DoNothing);
//            }

        
    }
}
