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
            var otherRobot = GetOtherRobotsVisionStatus(currentVisionSensorStatusList);

            if (!soccerBallVisionStatus.IsInsideVisionAngle)
            {
                //print("Can't see ball");
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnLeft, 1));
            }

            if (otherRobot.IsWithinDistance)
            {
                //print("I see another robot");
                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveBackward, RobotWheelAction.None, 2));
            }

            if (soccerBallVisionStatus.IsInsideVisionAngle && !soccerBallVisionStatus.IsWithinDistance)
            {
                //print("ball not within distance but in angle");

                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.MoveForward, RobotWheelAction.None, 3));
            }

            // Look for the goal when we have the ball and it's not in our vision.
            if (soccerBallVisionStatus.IsInsideVisionAngle && soccerBallVisionStatus.IsWithinDistance)
            {
               // print("goal not inside vision and have ball");

                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.None, RobotMotorAction.None, RobotWheelAction.TurnRight, 4));
            }

            // Move towards the goal when it's in our vision and we have the ball
            if (soccerBallVisionStatus.IsWithinDistance && otherRobot.IsInsideVisionAngle)
            {
               // print("goal inside vision and have ball");

                actionStateList.Add(new RobotActionState(RobotArmAction.None, RobotLegAction.KickForward, RobotMotorAction.None, RobotWheelAction.None, 5));
            }

            return actionStateList;
        }
    }
}