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
            {
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnLeft, 1));
            }
            
            if (soccerBallVisionStatus.IsInsideVisionAngle && !soccerBallVisionStatus.IsWithinDistance)
            {
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveForward, RobotWheelAction.None, 2));
            }
            
            if (soccerBallVisionStatus.IsInsideVisionAngle && soccerBallVisionStatus.IsWithinDistance && !awayGoalVisionStatus.IsInsideVisionAngle)
            {
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnLeft, 3));
            }
            
            if (soccerBallVisionStatus.IsInsideVisionAngle && soccerBallVisionStatus.IsWithinDistance && awayGoalVisionStatus.IsInsideVisionAngle)
            {
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.KickForward, RobotMotorAction.None, RobotWheelAction.None, 4));
            }

            return actionStateList;
        }
    }
}