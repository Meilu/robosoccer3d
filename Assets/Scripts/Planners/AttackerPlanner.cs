using System.Collections.Generic;
using System.Linq;
using DataModels;
using RobotActionStates;

namespace Planners
{
    public class AttackerPlanner : RobotPlanner
    {
        protected override IList<RobotActionState> ExecutePlan(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList)
        {
            var actionStateList = new List<RobotActionState>();
            var soccerBallVisionStatus = GetSoccerBallVisionStatus(currentVisionSensorStatusList);
            var ownGoalVisionStatus = GetOwnGoalVisionStatus(currentVisionSensorStatusList);
            var awayGoalVisionStatus = GetAwayGoalVisionStatus(currentVisionSensorStatusList);
            
            if (!soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnLeft, 1));

            if (soccerBallVisionStatus.IsInsideVisionAngle && !soccerBallVisionStatus.IsWithinDistance)
            {
                print("ball not within distance but in angle");

                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveForward, RobotWheelAction.None, 2));
            }

            // Look for the goal when we have the ball and it's not in our vision.
            if (soccerBallVisionStatus.IsInsideVisionAngle && soccerBallVisionStatus.IsWithinDistance && !awayGoalVisionStatus.IsInsideVisionAngle)
            {
                print("goal not inside vision and have ball");

                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnRight, 1));
            }

            // Move towards the goal when it's in our vision and we have the ball
            if (soccerBallVisionStatus.IsInsideVisionAngle && soccerBallVisionStatus.IsWithinDistance && awayGoalVisionStatus.IsInsideVisionAngle)
            {
                print("goal inside vision and have ball");

                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.BoostForward, RobotWheelAction.None, 4));
            }

            return actionStateList;
        }
    }
}