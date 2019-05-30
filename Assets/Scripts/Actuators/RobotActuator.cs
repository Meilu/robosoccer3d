using System.Linq;
using System.Timers;
using RobotActionStates;
using UnityEngine;
using DataModels;
using Planners;
using UnityEngine.Serialization;

namespace Actuators
{
    public class RobotActuator : MonoBehaviour
    {
        public RobotActionState activeRobotActionState;

        private Rigidbody _rigidBody;
        private Animator _frontLegsAnimator;
        
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _frontLegsAnimator = transform.Find("frontLegs").GetComponent<Animator>();
            
            // Dont allow the robot to be rotate by the physics engine.
            _rigidBody.freezeRotation = true;
        }
        
        public void ExecuteRobotAction(RobotActionState robotActionState)
        {
            activeRobotActionState = robotActionState;
        }
        
        private void FixedUpdate()
        {
            if (activeRobotActionState == null)
                return;
            
            switch (activeRobotActionState.MotorAction)
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
            
            switch (activeRobotActionState.WheelAction)
            {
                case RobotWheelAction.TurnLeft:
                    TurnLeft();
                    break;
                case RobotWheelAction.TurnRight:
                    TurnRight();
                    break;
            }

            switch (activeRobotActionState.LegAction)
            {
                case RobotLegAction.KickForward:
                    KickForward();
                    break;  
            }
        }

        //move motors
        private void MoveRight()
        {      
            _rigidBody.velocity = transform.right * 100.0f * Time.deltaTime;
        }

        private void MoveLeft()
        {
            _rigidBody.velocity = -transform.right * 100.0f * Time.deltaTime;
        }

        private void MoveForward()
        {
            _rigidBody.velocity = transform.forward * 100.0f * Time.deltaTime;
        }
        
        private void BoostForward()
        {
            _rigidBody.velocity = transform.forward * 120.0f * Time.deltaTime;
        }
        private void MoveBackward()
        {
            _rigidBody.velocity = -transform.forward * 100.0f * Time.deltaTime;
        }
        private void TurnRight()
        {
            transform.Rotate(Vector3.up * (120.0f * Time.deltaTime));
        }

        private void TurnLeft()
        {
            transform.Rotate(Vector3.up * (-120.0f * Time.deltaTime));
        }
        
        private void KickForward()
        {
            _frontLegsAnimator.Play("kickFrontLegsForward");
        }
    }
}