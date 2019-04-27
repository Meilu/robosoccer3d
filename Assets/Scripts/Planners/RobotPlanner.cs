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
        private readonly Queue<RobotAction> _robotActionQueue = new Queue<RobotAction>();
        
        void Start()
        {
            _robotVisionActuator = transform.GetComponent<RobotActuator>();
            _robotVisionSensor = transform.Find(Settings.RobotFieldOfViewObjectName).GetComponent<RobotVisionSensor>();
            
            InitializeObjectsOfInterestSubscriptions();
        }

        private void Update()
        {
            // First, check if there is not an action executing already
            if (_robotVisionActuator.HasActiveRobotAction())
                return;           
            
            // Check if there are items in the queue at this moment.
            if (!_robotActionQueue.Any())
                return;
            
            // There is an action available and no timer is currently running, take it from the queue and send it to the actuator :)
            var robotActionToExecute = _robotActionQueue.Dequeue();
            
            // Execute the dequeued action.
            _robotVisionActuator.ExecuteRobotAction(robotActionToExecute);
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

        /// <summary>
        /// This is the event handler that is called whenever a property of an object of interest changes.
        /// Here we will determine the action we need to take based on the new state of the visionstatus :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ObjectOfInterestPropertyChangeHandler(object sender, EventArgs args)
        {
            var objectOfInterestStatus = (ObjectOfInterestVisionStatus) sender;

            // Check if we have new actions to add to the queue :)
            var robotActionsToQueue = DetermineRobotActionType(objectOfInterestStatus);
            
            // Add all of the new actions to the queue.
            if (robotActionsToQueue != null && robotActionsToQueue.Any())
            {
                robotActionsToQueue.ForEach(x => _robotActionQueue.Enqueue(x));
                
            }
        }

        /// <summary>
        /// This function will determine based on the object type what robotaction to create. 
        /// </summary>
        /// <param name="objectOfInterestVisionStatus"></param>
        /// <returns></returns>
        private List<RobotAction> DetermineRobotActionType(ObjectOfInterestVisionStatus objectOfInterestVisionStatus)
        {
            // All action logic related to the soccerball
            if (objectOfInterestVisionStatus.ObjectName == Settings.SoccerBallObjectName)
                return DetermineRobotActionForSoccerBallChange(objectOfInterestVisionStatus);
            
            return null;
        }

        /// <summary>
        /// This function contains all logic for creating a robotaction based on the current status of the soccerball object.
        /// </summary>
        /// <param name="objectOfInterestVisionStatus"></param>
        /// <returns></returns>
        private List<RobotAction> DetermineRobotActionForSoccerBallChange(ObjectOfInterestVisionStatus objectOfInterestVisionStatus)
        {
            // If the soccerBall is within distance, move towards it.
            if (objectOfInterestVisionStatus.IsInsideVisionAngle)
                return new List<RobotAction>()
                {
                    new MoveForwardAction(),
                    new MoveBackwardAction(),
                    new TurnLeftAction(),
                    new MoveForwardAction(),
                    new TurnRightAction(),
                    new MoveForwardAction(),
                    new MoveBackwardAction()
                };

            return null;
        }


        
    }
}
