using UnityEngine;

namespace Actuators
{
    public class RobotActuator : MonoBehaviour
    {
        // private RobotMotorAction _activeRobotMotorAction;

        private Rigidbody _rigidbody;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            // Dont allow the robot to be rotate by the physics engine.
            _rigidbody.freezeRotation = true;
        }
        
        public void MoveForward()
        {
            _rigidbody.velocity = transform.forward * 100.0f * Time.deltaTime;
        }

        public void MoveBackward()
        {
            transform.Translate(Vector3.back * 1.0f * Time.deltaTime);
        }

        public void TurnRight()
        {
            transform.Rotate(Vector3.up * (150.0f * Time.deltaTime));
        }

        public void TurnLeft()
        {
            transform.Rotate(Vector3.up * (-150.0f * Time.deltaTime));
        }
        
        public void KickForward()
        {
            _rigidbody.velocity = Vector3.zero;
        }

//        private RobotWheelAction _activeRobotWheelAction;
//        private RobotLegAction _activeRobotLegAction;
//
//        float timeBetweenTriggers = 0.01f;
//        private bool actiontriggered = false;
//
//        private float? rotationBefore45DegreesTurn;
//        private bool hasExecutedMove;
//        private Rigidbody _rigidbody;

//        public void TriggerAction(RobotMotorAction robotAction)
//        {
//            if (!actiontriggered && _activeRobotMotorAction != robotAction)
//            {
//                _activeRobotMotorAction = robotAction;
//                actiontriggered = true;
//                timeBetweenTriggers = 0.01f;
//            }
//        }

//        public void TriggerAction(RobotWheelAction robotAction)
//        {
//            if (!actiontriggered && _activeRobotWheelAction != robotAction)
//            {
//                _activeRobotWheelAction = robotAction;
//                actiontriggered = true;
//                timeBetweenTriggers = 0.01f;
//            }
//        }

//        public void TriggerAction(RobotLegAction robotAction)
//        {
//            if (!actiontriggered && _activeRobotLegAction != robotAction)
//            {
//                _activeRobotLegAction = robotAction;
//                actiontriggered = true;
//                //We can make this action switch slower?
//                timeBetweenTriggers = 0.1f;
//            }
//        }

//        void FixedUpdate()
//        {

//            if (actiontriggered)
//            {
//                timeBetweenTriggers -= Time.deltaTime;
//            }
//
//            if (timeBetweenTriggers <= 0)
//            {
//                actiontriggered = false;
//            }
//
//            switch (_activeRobotMotorAction)
//            {
//                case RobotMotorAction.MoveForward:
//                    // print("Moving forward");
//                    MoveForward();
//                    break;
//                case RobotMotorAction.MoveBackward:
//                    // print("Moving backward");
//                    MoveBackward();
//                    break;
//                case RobotMotorAction.DoNothing:
//                    //stop moving
//                    DoNothing();
//                    break;
//                default:
//                    //stop moving
//                    DoNothing();
//                    break;
//            }
//
//            switch (_activeRobotWheelAction)
//            {
//                case RobotWheelAction.TurnRight:
//                    // print("Turning right");
//                    TurnRight();
//                    break;
//                case RobotWheelAction.TurnLeft:
//                    // print("Turning left");
//                    TurnLeft();
//                    break;
//                case RobotWheelAction.DoNothing:
//                    //stop moving
//                    DoNothing();
//                    break;
//                default:
//                    //stop moving
//                    DoNothing();
//                    break;
//            }
//
//            switch (_activeRobotLegAction)
//            {
//                case RobotLegAction.KickForward:
//                    print("Shooting");
//                    KickForward();
//                    break;
//                case RobotLegAction.DoNothing:
//                    //stop moving
//                    DoNothing();
//                    break;
//                default:
//                    //stop moving
//                    DoNothing();
//                    break;
//            }
//        }


    }
}