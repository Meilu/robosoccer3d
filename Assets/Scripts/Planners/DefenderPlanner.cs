using System.Collections.Generic;
using System.Linq;
using DataModels;
using RobotActionStates;

namespace Planners
{
    public class DefenderPlanner : RobotPlanner
    {
        protected override IList<RobotActionState> ExecutePlan(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList)
        {
            var actionStateList = new List<RobotActionState>();
            var soccerBallVisionStatus = currentVisionSensorStatusList.First(x => x.ObjectName == Settings.SoccerBallObjectName);
            var HomeGoalLineVisionStatus = currentVisionSensorStatusList.First(x => x.ObjectName == Settings.HomeGoalLine);

            if (!soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnLeft, 1));
    
            if (soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveForward, RobotWheelAction.None, 2));
    
            if (soccerBallVisionStatus.IsWithinDistance && !soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveBackward, RobotWheelAction.None, 3));

            if (soccerBallVisionStatus.IsWithinDistance && soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.BoostForward, RobotWheelAction.TurnRight, 4));

            if (HomeGoalLineVisionStatus.IsInsideVisionAngle && soccerBallVisionStatus.IsWithinDistance && soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveLeft, RobotWheelAction.TurnRight, 5));
            
            return actionStateList;
        }
    }
}