using System.Collections.Generic;
using System.Linq;
using DataModels;
using RobotActionStates;

namespace Planners
{
    public class LazyPlanner : RobotPlanner
    {
        protected override IList<RobotActionState> ExecutePlan(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList)
        {
            var actionStateList = new List<RobotActionState>();
            var soccerBallVisionStatus = currentVisionSensorStatusList.First(x => x.ObjectName == Settings.SoccerBallObjectName);

            if (!soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState( RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveForward, RobotWheelAction.TurnLeft, 1));

            if (soccerBallVisionStatus.IsInsideVisionAngle)
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveForward, RobotWheelAction.TurnLeft, 1));

            if (soccerBallVisionStatus.IsWithinDistance)
                actionStateList.Add(new RobotActionState( RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveBackward, RobotWheelAction.None, 2));
            
            return actionStateList;
        }
    }
}