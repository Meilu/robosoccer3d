namespace RobotActions
{
    public class MoveForwardAction : RobotAction
    {
        public override float ActionDuration => 1000.0f;
        public override RobotArmAction ArmAction => RobotArmAction.None;
        public override RobotLegAction LegAction => RobotLegAction.None;
        public override RobotMotorAction MotorAction => RobotMotorAction.MoveForward;
        public override RobotWheelAction WheelAction => RobotWheelAction.None;
    }
}