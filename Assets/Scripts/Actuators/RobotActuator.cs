using System.Timers;
using RobotActions;
using UnityEngine;

namespace Actuators
{
    public class RobotActuator : MonoBehaviour
    {
        private RobotAction _activeRobotAction;

        private Rigidbody _rigidbody;
        private Timer _actionExecuteTimer; 
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            // Dont allow the robot to be rotate by the physics engine.
            _rigidbody.freezeRotation = true;
        }

        public bool HasActiveRobotAction()
        {
            return _actionExecuteTimer != null && _actionExecuteTimer.Enabled;
        }
        
        public void ExecuteRobotAction(RobotAction robotAction)
        {
            _activeRobotAction = robotAction;
            StartExecuteRobotActionTimer(robotAction);
        }

        private void StartExecuteRobotActionTimer(RobotAction robotAction)
        {
            _actionExecuteTimer = new Timer();
            _actionExecuteTimer.Elapsed += ExecuteRobotActionTimerElapsed;
            _actionExecuteTimer.Interval = robotAction.ActionDuration;
            _actionExecuteTimer.Start();
        }

        private void ExecuteRobotActionTimerElapsed(object source, ElapsedEventArgs e)
        { 
            _activeRobotAction = null;
            _actionExecuteTimer.Stop();
        }

        private void FixedUpdate()
        {
            if (_activeRobotAction == null)
                return;
            
            // Check what actions to execute based on the current active robot action..
            switch (_activeRobotAction.MotorAction)
            {
                case RobotMotorAction.MoveForward:
                    MoveForward();
                    break;
                case RobotMotorAction.MoveBackward:
                    MoveBackward();
                    break;
            }
            
            // Check what actions to execute based on the current active robot action..
            switch (_activeRobotAction.WheelAction)
            {
                case RobotWheelAction.TurnLeft:
                    TurnLeft();
                    break;
                case RobotWheelAction.TurnRight:
                    TurnRight();
                    break;
            }
        }

        private void MoveForward()
        {
            _rigidbody.velocity = transform.forward * 100.0f * Time.deltaTime;
        }

        private void MoveBackward()
        {
            transform.Translate(Vector3.back * 1.0f * Time.deltaTime);
        }

        private void TurnRight()
        {
            transform.Rotate(Vector3.up * (150.0f * Time.deltaTime));
        }

        private void TurnLeft()
        {
            transform.Rotate(Vector3.up * (-150.0f * Time.deltaTime));
        }
        
        private void KickForward()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}