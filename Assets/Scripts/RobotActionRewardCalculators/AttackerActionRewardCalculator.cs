using System.Collections.Generic;
using System.Linq;
using DataModels;
using UnityEngine;

namespace RobotActionRewardCalculators
{
    public class AttackerActionRewardCalculatorBehaviour : RobotActionRewardCalculatorBehaviour
    {
        protected override RobotActionRewardCalculator RobotActionRewardCalculator { get; }

        AttackerActionRewardCalculatorBehaviour()
        {
            RobotActionRewardCalculator = new AttackerActionRewardCalculator();
        }
    }

    public class AttackerActionRewardCalculator : RobotActionRewardCalculator
    {
        protected override List<RobotActionReward> CollectRewardsForCurrentVision(IList<ObjectOfInterestVisionStatus> visionStatuses, float distanceTravelled)
        {
            var soccerBallStatus = visionStatuses.FirstOrDefault(x => x.ObjectName == Settings.SoccerBallObjectName);
            var actionRewardList = new List<RobotActionReward>();
            
            if (soccerBallStatus == null)
                return actionRewardList;

            if (soccerBallStatus.IsWithinDistance)
            {
                actionRewardList.Add(new RobotActionReward(0.4f));
            }

            return actionRewardList;
        }
    }
}