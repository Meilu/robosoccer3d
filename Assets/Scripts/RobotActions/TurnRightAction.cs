namespace RobotActions
{
    public class TurnRightAction : RobotAction
    {
        public override float ActionDuration => 600.0f;
        public override RobotArmAction ArmAction => RobotArmAction.None;
        public override RobotLegAction LegAction => RobotLegAction.None;
        public override RobotMotorAction MotorAction => RobotMotorAction.None;
        public override RobotWheelAction WheelAction => RobotWheelAction.TurnRight;
    }
}