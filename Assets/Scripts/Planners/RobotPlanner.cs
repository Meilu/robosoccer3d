using System;
using System.Collections.Generic;
using Actuators;
using DataModels;
using RobotActionStates;
using Sensors;
using UnityEngine;


namespace Planners
{
    public abstract class RobotPlannerBehaviour : MonoBehaviour
    {
        private RobotActuator _robotVisionActuator;
        private RobotVisionSensorBehaviour _robotVisionSensor;
        protected abstract RobotPlanner RobotPlanner { get; }
        protected Robot RobotModel { get; set; }
        
        void Start()
        {
            _robotVisionActuator = transform.GetComponent<RobotActuator>();
            _robotVisionSensor = transform.Find(Settings.RobotFieldOfViewObjectName).GetComponent<RobotVisionSensorBehaviour>();
            
            InitializeObjectsOfInterestSubscriptions();
        }
        
        /// <summary>
        /// This is the event handler that is called whenever a property of an object of interest changes.
        /// Here we will determine the action we need to take based on the new state of the visionstatus
        /// </summary>
        private void ObjectOfInterestPropertyChangeHandler(object sender, EventArgs args)
        {
            RobotPlanner.DetermineRobotActionForCurrentSensors();
        }
        
        /// <summary>
        /// Binds event handlers to the properties of the object of interest.
        /// </summary>
        private void InitializeObjectsOfInterestSubscriptions()
        {
            // Subscribe to the events of the objects of interest.
            foreach (var objectOfInterestVisionStatus in _robotVisionSensor.objectOfInterestVisionStatuses)
            {
                objectOfInterestVisionStatus.IsInsideVisionAngleChangeEvent += ObjectOfInterestPropertyChangeHandler;
                objectOfInterestVisionStatus.IsWithinDistanceChangeEvent += ObjectOfInterestPropertyChangeHandler;
            }
        }
    }

    public abstract class RobotPlanner
    {
        private readonly IList<RobotActionState> _robotActionQueue = new List<RobotActionState>();
        protected abstract IList<RobotActionState> ExecutePlan(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList);

        public bool ShouldExecuteAction(RobotActionState activeRobotAction, RobotActionState robotActionToExecute)
        {
            return activeRobotAction != null && activeRobotAction.Weight > robotActionToExecute.Weight && IsVisionStillCurrent(activeRobotAction.VisionStatusOnCreate);
        }
        
        public bool IsVisionStillCurrent(IList<ObjectOfInterestVisionStatus> visionStatus)
        {
            return false;
        }
        
        public IList<RobotActionState> DetermineRobotActionForCurrentSensors()
        {
            return new List<RobotActionState>();
        }
    }
}