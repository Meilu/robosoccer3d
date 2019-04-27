namespace RobotActions
{
    public abstract class RobotAction
    {
        public abstract float ActionDuration { get; }
        
        public abstract RobotArmAction ArmAction { get; }
        public abstract RobotLegAction LegAction { get; }
        public abstract RobotMotorAction MotorAction { get; }
        public abstract RobotWheelAction WheelAction { get; }
    }
}