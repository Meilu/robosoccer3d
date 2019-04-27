namespace RobotActions
{
    public class MoveBackwardAction : RobotAction
    {
        public override float ActionDuration => 1000.0f;
        public override RobotArmAction ArmAction => RobotArmAction.None;
        public override RobotLegAction LegAction => RobotLegAction.None;
        public override RobotMotorAction MotorAction => RobotMotorAction.MoveBackward;
        public override RobotWheelAction WheelAction => RobotWheelAction.None;
    }
}