using System.Collections.Generic;
using System.Linq;
using DataModels;
using RobotActionStates;

namespace Planners
{
    public class KeeperPlanner : RobotPlanner
    {
        protected override IList<RobotActionState> ExecutePlan(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList)
        {
            var actionStateList = new List<RobotActionState>();
            var soccerBallVisionStatus = GetObjectVistionStatus(currentVisionSensorStatusList, Settings.SoccerBallObjectName);
            var ownGoalVisionStatus = GetObjectVistionStatus(currentVisionSensorStatusList, GetOwnGoalName());
            var awayGoalVisionStatus = GetAwayGoalVisionStatus(currentVisionSensorStatusList);

            if (!soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnLeft, 1));
    
            if (soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveForward, RobotWheelAction.None, 2));
    
            if (soccerBallVisionStatus.IsWithinDistance)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.BoostForward, RobotWheelAction.TurnRight, 3));

            if (ownGoalVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveLeft, RobotWheelAction.None, 4));

            if (awayGoalVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveLeft, RobotWheelAction.None, 4));
            

            return actionStateList;
        }
    }
}