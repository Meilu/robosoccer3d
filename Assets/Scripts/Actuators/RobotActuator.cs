using System.Linq;
using System.Timers;
using RobotActionStates;
using UnityEngine;

namespace Actuators
{
    public class RobotActuator : MonoBehaviour
    {
        public RobotActionState _activeRobotActionState;

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
        
        public void ExecuteRobotAction(RobotActionState robotActionState)
        {
            
            _activeRobotActionState = robotActionState;
        }

        /// <summary>
        /// Checks wether the given robotaction state is more important than the one that is currently active.
        /// </summary>
        /// <param name="robotActionState"></param>
        /// <returns></returns>
        private bool ShouldExecuteRobotActionState(RobotActionState robotActionState)
        {
            return _activeRobotActionState == null || robotActionState.Weight > _activeRobotActionState.Weight;
        }
        
        private void FixedUpdate()
        {
            if (_activeRobotActionState == null)
                return;
            
            // Check what actions to execute based on the current active robot action..
            switch (_activeRobotActionState.MotorAction)
            {
                case RobotMotorAction.MoveForward:
                    MoveForward();
                    break;
                case RobotMotorAction.MoveBackward:
                    MoveBackward();
                    break;
                case RobotMotorAction.BoostForward:
                    BoostForward();
                    break;
                case RobotMotorAction.MoveLeft:
                    MoveLeft();
                    break;
                case RobotMotorAction.MoveRight:
                    MoveRight();
                    break;
            }
            
            // Check what actions to execute based on the current active robot action..
            switch (_activeRobotActionState.WheelAction)
            {
                case RobotWheelAction.TurnLeft:
                    TurnLeft();
                    break;
                case RobotWheelAction.TurnRight:
                    TurnRight();
                    break;
            }
        }

        private void MoveRight()
        {      
            _rigidbody.velocity = transform.right * 100.0f * Time.deltaTime;
        }

        private void MoveLeft()
        {
            _rigidbody.velocity = -transform.right * 100.0f * Time.deltaTime;
        }

        private void MoveForward()
        {
            _rigidbody.velocity = transform.forward * 100.0f * Time.deltaTime;
        }
        
        private void BoostForward()
        {
            _rigidbody.velocity = transform.forward * 300.0f * Time.deltaTime;
        }

        private void MoveBackward()
        {
            _rigidbody.velocity = -transform.forward * 100.0f * Time.deltaTime;
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
        
        public static void DumpToConsole(object obj)
        {
            var output = JsonUtility.ToJson(obj, true);
            Debug.Log(output);
        }
    }
}