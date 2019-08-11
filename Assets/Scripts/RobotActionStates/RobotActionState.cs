using System.Collections.Generic;
using DataModels;

namespace RobotActionStates
{
    public class RobotActionState
    {
        public RobotArmAction ArmAction { get; }

        public RobotLegAction LegAction { get; }
        public RobotMotorAction MotorAction { get; }
        public RobotWheelAction WheelAction { get; }
        public float Weight = 0.0f;
        public IList<ObjectOfInterestVisionStatus> VisionStatusOnCreate { get; set;  }
        public RobotActionState(RobotArmAction armAction, RobotLegAction legAction, RobotMotorAction motorAction, RobotWheelAction wheelAction)
        {
            ArmAction = armAction;
            LegAction = legAction;
            MotorAction = motorAction;
            WheelAction = wheelAction;
        }
    }
}