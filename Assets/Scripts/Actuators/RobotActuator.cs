using DataModels;
using UnityEngine;

namespace Actuators
{
    public class RobotActuator : MonoBehaviour, IRobotActuator
    {
        public RobotActionState ActiveRobotActionState { get; internal set; }
        public bool IsBoosting => ActiveRobotActionState != null && ActiveRobotActionState.MotorAction == RobotMotorAction.BoostForward;

        public Rigidbody rigidBody;
        private Animator _frontLegsAnimator;
        
        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            _frontLegsAnimator = transform.Find("frontLegs").GetComponent<Animator>();
            
            // Dont allow the robot to be rotate by the physics engine.
            rigidBody.freezeRotation = true;
        }
        
        public void ExecuteRobotAction(RobotActionState robotActionState)
        {
            ActiveRobotActionState = robotActionState;
        }
        
        private void FixedUpdate()
        {
            if (ActiveRobotActionState == null)
                return;
            
            switch (ActiveRobotActionState.MotorAction)
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
            
            switch (ActiveRobotActionState.WheelAction)
            {
                case RobotWheelAction.TurnLeft:
                    TurnLeft();
                    break;
                case RobotWheelAction.TurnRight:
                    TurnRight();
                    break;
            }

            switch (ActiveRobotActionState.LegAction)
            {
                case RobotLegAction.KickForward:
                    KickForward();
                    break;  
            }
        }

        //move motors
        public void MoveRight()
        {      
            rigidBody.velocity = transform.right * 100.0f * Time.deltaTime;
        }

        public void MoveLeft()
        {
            rigidBody.velocity = -transform.right * 100.0f * Time.deltaTime;
        }

        public void MoveForward()
        {
            rigidBody.velocity = transform.forward * 100.0f * Time.deltaTime;
        }

        public void BoostForward()
        {
            rigidBody.velocity = transform.forward * 120.0f * Time.deltaTime;
        }

        public void MoveBackward()
        {
            rigidBody.velocity = -transform.forward * 100.0f * Time.deltaTime;
        }

        public void TurnRight()
        {
            transform.Rotate(Vector3.up * (120.0f * Time.deltaTime));
        }

        public void TurnLeft()
        {
            transform.Rotate(Vector3.up * (-120.0f * Time.deltaTime));
        }

        public void KickForward()
        {
            _frontLegsAnimator.Play("kickFrontLegsForward");
        }
    }
}