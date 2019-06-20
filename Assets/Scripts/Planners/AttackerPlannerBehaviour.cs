using System.Collections.Generic;
using System.Linq;
using DataModels;
using RobotActionStates;

namespace Planners
{
    public class AttackerPlannerBehaviour : RobotPlannerBehaviour
    {
        protected override RobotPlanner RobotPlanner { get; }

        AttackerPlannerBehaviour()
        {
            RobotPlanner = new AttackerPlanner();
        }
    }

    public class AttackerPlanner : RobotPlanner
    {
        protected override IList<RobotActionState> ExecutePlan(IList<ObjectOfInterestVisionStatus> currentVisionSensorStatusList)
        {
            // Hand out cookies based on the behaviour of the attacker here (machine learning)
            return new List<RobotActionState>();
        }
    }
}