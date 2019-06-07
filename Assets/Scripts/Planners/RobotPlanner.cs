using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Actuators;
using DataModels;
using RobotActionStates;
using Sensors;
using UnityEngine;
using Team = PhysReps.Team;
using Timer = System.Timers.Timer;

namespace Planners
{
    public abstract class RobotPlanner : MonoBehaviour
    {
        private RobotActuator _robotVisionActuator;
        private RobotVisionSensor _robotVisionSensor;
        private RobotMovementSensor _robotMovementSensor;
        
        // This holds the properties for this robot from the robotmodel. We can read the attack/defense speed etc from this model.
        public Robot RobotModel { get; set; }
        
        private readonly IList<RobotActionState> _robotActionQueue = new List<RobotActionState>();
        
        protected abstract IList<RobotActionState> ExecutePlan(
            IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList);

        void Start()
        {
            _robotVisionActuator = transform.GetComponent<RobotActuator>();
            _robotVisionSensor = transform.Find(Settings.RobotFieldOfViewObjectName).GetComponent<RobotVisionSensor>();
            _robotMovementSensor = transform.Find(Settings.RobotMovementStatusObjectName).GetComponent<RobotMovementSensor>();
            
            InitializeObjectsOfInterestSubscriptions();
            DetermineRobotActionForCurrentSensors();
    
        }

        private void Update()
        {
            // Check if there are items in the queue at this moment.
            if (!_robotActionQueue.Any())
                return;

            // Get the first robotactionstate ordered by the highest weight first.
            var robotActionToExecute = _robotActionQueue.OrderByDescending(x => x.Weight).First();
            var activeRobotAction = _robotVisionActuator.activeRobotActionState;

            // Before sending the new action, make sure the current one is not more important (and also still true).
            if (activeRobotAction != null && activeRobotAction.Weight > robotActionToExecute.Weight && IsVisionStillCurrent(activeRobotAction.VisionStatusOnCreate))
                // The current vision is still current and also more important, dont continue.
                return;

            // Save the state of the current vision on the action before we execute it :)
            // This way we have a small history of the state he had before we transitioned to it's new state (maybe come in handy)
            robotActionToExecute.VisionStatusOnCreate = GetCurrentVisionStatusList();
            
            // Execute the dequeued action.
            _robotVisionActuator.ExecuteRobotAction(robotActionToExecute);
        }

        /// <summary>
        /// Binds event handlers to the properties of the object of interest.
        /// </summary>
        private void InitializeObjectsOfInterestSubscriptions()
        {
            // Subscribe to the events of the objects of interest.
            foreach (var objectOfInterestVisionStatus in _robotVisionSensor.objectsOfInterestStatus)
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
            DetermineRobotActionForCurrentSensors();
        }

        private void DetermineRobotActionForCurrentSensors()
        {
            // Check what state belongs to our current vision.
            var robotActionStatesForCurrentVision = DetermineRobotActionStateForCurrentVision();

            // Clear the current queue and add all new items based on the new vision.
            _robotActionQueue.Clear();
            
            foreach (var robotActionState in robotActionStatesForCurrentVision)
            {
                _robotActionQueue.Add(robotActionState);
            }
        }
        
        private IList<RobotActionState> DetermineRobotActionStateForCurrentVision()
        {
            var currentVisionSensorStatusList = GetCurrentVisionStatusList();
            
            return ExecutePlan(currentVisionSensorStatusList);
        }

        private IList<ObjectOfInterestVisionStatus> GetCurrentVisionStatusList()
        {
            IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList = new List<ObjectOfInterestVisionStatus>();

            _robotVisionSensor.objectsOfInterestStatus.ForEach(x => currentVisionSensorStatusList.Add(x.Copy()));

            return currentVisionSensorStatusList;
        }
        
        private bool IsVisionStillCurrent(IList<ObjectOfInterestVisionStatus> visionStatus)
        {
            return _robotVisionSensor.objectsOfInterestStatus.SequenceEqual(visionStatus);
        }

        private TeamSide GetTeamSide()
        {
            return transform.parent.GetComponent<Team>().teamSide;
        }

        protected string GetOwnGoalName()
        {
            return GetTeamSide() == TeamSide.Home ? Settings.HomeGoalLine : Settings.AwayGoalLine;
        }

        protected string GetAwayGoalName()
        {
            return GetTeamSide() == TeamSide.Home ? Settings.AwayGoalLine : Settings.HomeGoalLine;
        }
        
        protected ObjectOfInterestVisionStatus GetOwnGoalVisionStatus(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList)
        {
            return currentVisionSensorStatusList.FirstOrDefault(x => x.ObjectName == GetOwnGoalName());
        }
        
        protected ObjectOfInterestVisionStatus GetAwayGoalVisionStatus(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList)
        {
            return currentVisionSensorStatusList.FirstOrDefault(x => x.ObjectName == GetAwayGoalName());
        }
        
        protected ObjectOfInterestVisionStatus GetSoccerBallVisionStatus(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList)
        {
            return currentVisionSensorStatusList.First(x => x.ObjectName == Settings.SoccerBallObjectName);
        }  
    }
}