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
            return new List<RobotActionState>();
        }
    }
}