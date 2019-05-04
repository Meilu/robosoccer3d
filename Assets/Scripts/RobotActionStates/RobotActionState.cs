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
        public RobotActionState(RobotArmAction armAction, RobotLegAction legAction, RobotMotorAction motorAction, RobotWheelAction wheelAction, float weight)
        {
            ArmAction = armAction;
            LegAction = legAction;
            MotorAction = motorAction;
            WheelAction = wheelAction;
            
            // The weight of the state will determine it's importance relative to other states. The highest state will always be picked first.
            Weight = weight;
        }
    }
}