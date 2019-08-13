using System.Collections.Generic;

namespace DataModels
{
    public class RobotActionReward
    {
        public float RewardAmount { get; set; }

        public RobotActionReward(float rewardAmount)
        {
            RewardAmount = rewardAmount;
        }
    }
}