using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Actuators;
using DataModels;
using RobotActions;
using Sensors;
using UnityEngine;
using Timer = System.Timers.Timer;

namespace Planners
{
    public class RobotPlanner : MonoBehaviour
    {
        private RobotActuator _robotVisionActuator;
        private RobotVisionSensor _robotVisionSensor;

        private Queue<RobotAction> _robotActionQueue = new Queue<RobotAction>();

        private Timer _actionExecuteTimer; 
        void Start()
        {
            _robotVisionActuator = transform.GetComponent<RobotActuator>();
            _robotVisionSensor = transform.Find(Settings.RobotFieldOfViewObjectName).GetComponent<RobotVisionSensor>();
            
            InitializeObjectsOfInterestSubscriptions();
        }

        private void FixedUpdate()
        {
            
        }

        private void Update()
        {
            // Check if there are items in the queue at this moment
            if (!_robotActionQueue.Any())
                return;
            
            // Check if there is no timer for an action running already
            if (_actionExecuteTimer != null && _actionExecuteTimer.Enabled)
                return;
            
            // There is an action available and no timer is currently running, take it from the queue and send it to the actuator :)
            var robotActionToExecute = _robotActionQueue.Dequeue();
        }

        /// <summary>
        /// Binds event handlers to the properties of the object of interest.
        /// </summary>
        private void InitializeObjectsOfInterestSubscriptions()
        {
            // Subscribe to the events of the objects of interest.
            foreach (var objectOfInterestVisionStatus in _robotVisionSensor.objectsOfInterestVisionStatus)
            {
                objectOfInterestVisionStatus.IsInsideVisionAngleChangeEvent += ObjectOfInterestPropertyChangeHandler;
                objectOfInterestVisionStatus.IsWithinDistanceChangeEvent += ObjectOfInterestPropertyChangeHandler;
            }
        }

        private void ObjectOfInterestPropertyChangeHandler(object sender, EventArgs args)
        {
            var objectOfInterestStatus = (ObjectOfInterestVisionStatus) sender;

            // Check if we have new actions to add to the queue :)
            var robotActionsToQueue = DetermineRobotActionForSoccerBallChange(objectOfInterestStatus);
            
            // Add all of the new actions to the queue.
            if (robotActionsToQueue.Any())
            {
                robotActionsToQueue.ForEach(x => _robotActionQueue.Enqueue(x));
                
            }
        }

        private List<RobotAction> DetermineRobotActionType(ObjectOfInterestVisionStatus objectOfInterestVisionStatus)
        {
            // All action logic related to the soccerball
            if (objectOfInterestVisionStatus.ObjectName != Settings.SoccerBallObjectName)
                return DetermineRobotActionForSoccerBallChange(objectOfInterestVisionStatus);
            
            return null;
        }

        private List<RobotAction> DetermineRobotActionForSoccerBallChange(ObjectOfInterestVisionStatus objectOfInterestVisionStatus)
        {
            // If the soccerball is within distance
            if (objectOfInterestVisionStatus.IsWithinDistance)
                return new List<RobotAction>()
                {
                    new MoveForwardAction()
                };

            return null;
        }

//        private void TriggerMoveForward()
//        {
//            _robotVisionActuator.TriggerAction(RobotMotorAction.MoveForward);
//        }
//
//        private void TriggerTurnLeft()
//        {
//            _robotVisionActuator.TriggerAction(RobotWheelAction.TurnLeft);
//        }
//        
//        private void TriggerTurnRight()
//        {
//            _robotVisionActuator.TriggerAction(RobotWheelAction.TurnLeft);
//        }
//
//        private void TriggerKickForward()
//        {
//            _robotVisionActuator.TriggerAction(RobotLegAction.KickForward);
//        }

        


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
