using System.Linq;
using Actuators;
using DataModels;
using MLAgents;
using RobotActionRewardCalculators;
using Sensors;
using UnityEngine;

namespace MachineLearning
{
    public class RobotPlayerAgent : Agent
    {
        private RobotActuator _robotActuator;
        private RobotVisionSensorBehaviour _robotVisionSensor;
        private RobotActionRewardCalculatorBehaviour _robotActionRewardCalculator;

        private Vector3 originalPosition;
        // Start is called before the first frame update
        void Start()
        {
            _robotActuator = gameObject.GetComponent<RobotActuator>();
            _robotVisionSensor = gameObject.transform.Find("robotFieldOfView").GetComponent<RobotVisionSensorBehaviour>();
            _robotActionRewardCalculator = gameObject.GetComponent<RobotActionRewardCalculatorBehaviour>();

            originalPosition = transform.position;
        }

        public override void AgentReset()
        {
            // transform.position = originalPosition;
        }

        public override void AgentAction(float[] action, string actionText)
        {
            var motorAction = (RobotMotorAction) (int) action[0];
            var wheelAction = (RobotWheelAction) (int) action[1];
            var legAction = (RobotLegAction) (int) action[2];
            var armAction = (RobotArmAction) (int) action[3];

            var robotActionState = new RobotActionState(RobotArmAction.None, RobotLegAction.None, motorAction, RobotWheelAction.None);
            
            _robotActuator.ExecuteRobotAction(robotActionState);

            var rewards = _robotActionRewardCalculator.CollectActionRewards();
            
            if (!rewards.Any())
                return;
            
            rewards.ForEach(x => AddReward(x.RewardAmount));
        }

        public override void CollectObservations()
        {
            // Only collect the status of the soccerball for now.
            var soccerBall = _robotVisionSensor.objectOfInterestVisionStatuses.FirstOrDefault(x => x.ObjectName == Settings.SoccerBallObjectName);

            CollectObjectOfInterestVisionStatus(soccerBall);
        }
        void CollectObjectOfInterestVisionStatuses() {
            foreach(ObjectOfInterestVisionStatus status in _robotVisionSensor.objectOfInterestVisionStatuses) {
                CollectObjectOfInterestVisionStatus(status);
            }
        }

        void CollectObjectOfInterestVisionStatus(ObjectOfInterestVisionStatus status) {
            AddVectorObs(status.IsInsideVisionAngle);
            AddVectorObs(status.IsWithinDistance);
            AddVectorObs(status.DistanceFromObject);
        }
    }
}
