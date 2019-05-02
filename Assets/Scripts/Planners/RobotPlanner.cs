using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Actuators;
using DataModels;
using RobotActionStates;
using Sensors;
using UnityEngine;
using Timer = System.Timers.Timer;

namespace Planners
{
    public class RobotPlanner : MonoBehaviour
    {
        private RobotActuator _robotVisionActuator;
        private RobotVisionSensor _robotVisionSensor;
        private readonly IList<RobotActionState> _robotActionQueue = new List<RobotActionState>();
        
        void Start()
        {
            _robotVisionActuator = transform.GetComponent<RobotActuator>();
            _robotVisionSensor = transform.Find(Settings.RobotFieldOfViewObjectName).GetComponent<RobotVisionSensor>();
            
            InitializeObjectsOfInterestSubscriptions();
        }

        private void Update()
        {            
            // Check if there are items in the queue at this moment.
            if (!_robotActionQueue.Any())
                return;
            
            // Get the first robotactionstate ordered by the highest weight first.
            var robotActionToExecute = _robotActionQueue.OrderByDescending(x => x.Weight).FirstOrDefault();
            var activeRobotAction = _robotVisionActuator._activeRobotActionState;
            
            // Before sending the new action, make sure the current one is not more important (and also still true).
            if (activeRobotAction != null && activeRobotAction.Weight > robotActionToExecute.Weight && IsVisionStillCurrent(activeRobotAction.VisionStatusOnCreate))
                // The current vision is still current and also more important, dont continue.
                return;
            
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
        private void ObjectOfInterestPropertyChangeHandler(object sender, EventArgs args)
        {
            // Check what state belongs to our current vision.
            var robotActionStateForCurrentVision = DetermineRobotActionStateForCurrentVision();
            
            // Clear the current queue and add all new items based on the new vision.
            _robotActionQueue.Clear();
            robotActionStateForCurrentVision.ForEach(x => _robotActionQueue.Add(x));
        }

        private List<RobotActionState> DetermineRobotActionStateForCurrentVision()
        {
            IList<ObjectOfInterestVisionStatus> clonedList = new List<ObjectOfInterestVisionStatus>();
            var actionStateList = new List<RobotActionState>();
            
            // Clone the current vision so that we can save it's state for any states created
            _robotVisionSensor.objectsOfInterestVisionStatus.ForEach(x => clonedList.Add(x.Copy()));
            var activeState = _robotVisionActuator._activeRobotActionState;
            
            // Get the status of the soccerball. 
            var soccerBallVisionStatus = clonedList.First(x => x.ObjectName == Settings.SoccerBallObjectName);
            var HomeGoalLineVisionStatus = clonedList.First(x => x.ObjectName == Settings.HomeGoalLine);
            
            if (!soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(clonedList, RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnLeft, 1));
            
            if (soccerBallVisionStatus.IsInsideVisionAngle && !soccerBallVisionStatus.IsWithinDistance)
                actionStateList.Add(new RobotActionState(clonedList, RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveForward, RobotWheelAction.None, 2));
            
            
            if (soccerBallVisionStatus.IsWithinDistance)
                actionStateList.Add(new RobotActionState(clonedList, RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveLeft, RobotWheelAction.None, 4));
                
            
            if (soccerBallVisionStatus.IsInsideVisionAngle && soccerBallVisionStatus.IsWithinDistance)
                actionStateList.Add(new RobotActionState(clonedList, RobotArmAction.None, RobotLegAction.None, RobotMotorAction.BoostForward, RobotWheelAction.None, 3));

            if (HomeGoalLineVisionStatus.IsInsideVisionAngle && soccerBallVisionStatus.IsWithinDistance)
                actionStateList.Add(new RobotActionState(clonedList, RobotArmAction.None, RobotLegAction.None, RobotMotorAction.BoostForward, RobotWheelAction.None, 5));
            
            return actionStateList;
        }

        private bool IsVisionStillCurrent(IList<ObjectOfInterestVisionStatus> visionStatus)
        {
            return _robotVisionSensor.objectsOfInterestVisionStatus.SequenceEqual(visionStatus);
        }

    }
}
