using System;
using System.Collections.Generic;
using DataModels;
using Sensors;
using UnityEngine;

namespace RobotActionRewardCalculators
{
    public delegate void ObjectsOfInterestHaveChangedEventHandler();

    public abstract class RobotActionRewardCalculatorBehaviour : MonoBehaviour
    {
        private RobotVisionSensorBehaviour _robotVisionSensor;
        private RobotTravelDistanceSensorBehaviour _robotTravelDistanceSensor;
        protected abstract RobotActionRewardCalculator RobotActionRewardCalculator { get; }
        
        // A single event that get's thrown whenever one of the statuses of any of the objects of interest change.
        // This way i dont have to subscribe to all objects of interest again in other classes but can just subscribe to this one.
        public event ObjectsOfInterestHaveChangedEventHandler ObjectsOfInterestHaveChanged;

        void Start()
        {
            _robotVisionSensor = transform.Find(Settings.RobotFieldOfViewObjectName).GetComponent<RobotVisionSensorBehaviour>();
            _robotTravelDistanceSensor = GetComponent<RobotTravelDistanceSensorBehaviour>();
            
            InitializeObjectsOfInterestSubscriptions();
        }
        
        private void ObjectOfInterestPropertyChangeHandler(object sender, EventArgs args)
        {
          //  RobotActionRewardCalculator.CollectActionRewardsForSensors(_robotVisionSensor.objectOfInterestVisionStatuses);

            ObjectsOfInterestHaveChanged?.Invoke();
        }

        public List<RobotActionReward> CollectActionRewards()
        {
            return RobotActionRewardCalculator.CollectActionRewardsForSensors(_robotVisionSensor.objectOfInterestVisionStatuses, _robotTravelDistanceSensor.distanceTravelled);
        }
        
        private void InitializeObjectsOfInterestSubscriptions()
        {
            foreach (var objectOfInterestVisionStatus in _robotVisionSensor.objectOfInterestVisionStatuses)
            {
                objectOfInterestVisionStatus.IsInsideVisionAngleChangeEvent += ObjectOfInterestPropertyChangeHandler;
                objectOfInterestVisionStatus.IsWithinDistanceChangeEvent += ObjectOfInterestPropertyChangeHandler;
            }
        }
    }

    public abstract class RobotActionRewardCalculator
    {
        // Add abstract collect functions for each sensor we have.
        protected abstract List<RobotActionReward> CollectRewardsForCurrentVision(IList<ObjectOfInterestVisionStatus> visionStatuses, float distanceTravelled);

        public List<RobotActionReward> CollectActionRewardsForSensors(IList<ObjectOfInterestVisionStatus> visionStatuses, float distanceTravelled)
        {
            // Place the collection of all sensors here, so far we only have one sensor so just return right away.
            return CollectRewardsForCurrentVision(visionStatuses, distanceTravelled);
        }
    }
}