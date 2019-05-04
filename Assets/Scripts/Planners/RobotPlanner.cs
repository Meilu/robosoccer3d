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
    // This class is marked abstract because it has an incomplete implementation by itself,
    // this way we enforce that it can only be used by instantiating derived classes (defender, midfielder, attacker)
    // Basically, this class is now only intended to provide the base functionality of creating a queue etc.
    // the logic for what actions to take will now be determined in the derived classes.
    // please check out a defenderplan and attackerplanner class for an example
    public abstract class RobotPlanner : MonoBehaviour
    {
        private RobotActuator _robotVisionActuator;
        private RobotVisionSensor _robotVisionSensor;
        private RobotMovementSensor _robotMovementSensor;
        
        private readonly IList<RobotActionState> _robotActionQueue = new List<RobotActionState>();

        // This is an abstract function definition for the executeplan function.
        // Abstract means it will not have an implementation in this base class,
        // but tells us that any derived classes (defenderplanner, attackerplanner etc) are enforced to implement this method.
        // I decided to make this function abstract and have the logic inside derived classes because this way we won't have one big file that contain all executeplan logic.
        // Hope that makes sense. Please check out one of the derived classes (defenderplanner, attackerplanner) for the implementations.
        protected abstract IList<RobotActionState> ExecutePlan(
            IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList);

        void Start()
        {
            _robotVisionActuator = transform.GetComponent<RobotActuator>();
            _robotVisionSensor = transform.Find(Settings.RobotFieldOfViewObjectName).GetComponent<RobotVisionSensor>();
            _robotMovementSensor = transform.Find(Settings.RobotMovementStatusObjectName).GetComponent<RobotMovementSensor>();
            
            InitializeObjectsOfInterestSubscriptions();
        }

        private void Update()
        {
            // Check if there are items in the queue at this moment.
            if (!_robotActionQueue.Any())
                return;

            // Get the first robotactionstate ordered by the highest weight first.
            var robotActionToExecute = _robotActionQueue.OrderByDescending(x => x.Weight).First();
            var activeRobotAction = _robotVisionActuator._activeRobotActionState;

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
            var currentVisionSensorStatusList = this.GetCurrentVisionStatusList();
            
            return ExecutePlan(currentVisionSensorStatusList);
        }

        private IList<ObjectOfInterestVisionStatus> GetCurrentVisionStatusList()
        {
            IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList = new List<ObjectOfInterestVisionStatus>();

            // Create a copy of the status for all objects of interest for the vision.
            _robotVisionSensor.objectsOfInterestVisionStatus.ForEach(x => currentVisionSensorStatusList.Add(x.Copy()));

            return currentVisionSensorStatusList;
        }
        
        private bool IsVisionStillCurrent(IList<ObjectOfInterestVisionStatus> visionStatus)
        {
            return _robotVisionSensor.objectsOfInterestStatus.SequenceEqual(visionStatus);
        }
    }
}