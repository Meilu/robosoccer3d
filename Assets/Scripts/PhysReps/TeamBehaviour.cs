using System.Collections.Generic;
using System.Linq;
using DataModels;
using Planners;
using UnityEngine;

namespace PhysReps
{
    
    public class TeamBehaviour : MonoBehaviour
    {
        public GameObject robotPrefab;
        
        private IList<FormationPosition> _formationsPositions;
        private IList<Robot> _robots;
        private HumbleTeamBehaviour _humbleTeam;

        public void InitializeTeam(Team team)
        {
            _humbleTeam = new HumbleTeamBehaviour(team.Side);
            
            // Create some robots (for now hardcoded, but in the future the player will be able to set these from the ui and add any robot he wishes :))
            _formationsPositions = _humbleTeam.GetFormationPositions(3, GetComponent<BoxCollider>().bounds.size, robotPrefab.transform.GetComponent<BoxCollider>().size, -0.4f).ToList();

            _robots = new List<Robot>()
            {
                new Robot()
                {
                    Number = 1,
                    Name = "Alpha",
                    Speed = 1f,
                    AttackPower = 5f,
                    DefensePower = 20f,
                    TeamPosition =  TeamPosition.Attacker
                }
            };
            
            foreach (var robotModel in _robots)
            {
                var positionVector = _humbleTeam.FindFormationPositionVectorBySquadNumber(_formationsPositions, robotModel.Number);
                var robot = Instantiate(robotPrefab, transform, false);
                robot.transform.localPosition = new Vector3(positionVector.x, positionVector.y, positionVector.z);
                
                // Save all of the properties of the robotmodel onto the prefab, so we can access them during the game if needed:)
                // Creating planners is commented out for now because we do not know yet how to implement them along with machine learning
//                RobotPlanner plannerComponent = _team.AddPlannerComponentToRobot(robotModel.TeamPosition, robot);
//                plannerComponent.RobotModel = robotModel;
            }
        }
    }
    
    // Placed all logic in a seperate class so we can unit test it without annoying unity dependencies.
    public class HumbleTeamBehaviour
    {
        private TeamSide _teamSide;

        public HumbleTeamBehaviour(TeamSide teamSide)
        {
            _teamSide = teamSide;
        }
        public IEnumerable<FormationPosition> GetFormationPositions(int maxSquadNumber, Vector3 colliderBounds, Vector3 robotPrefabSize, float robotYAxisPosition)
        {
            for (var i = 0; i < maxSquadNumber; i++)
                yield return CreateFormationPositionForSquadNumber(i, colliderBounds, robotPrefabSize, robotYAxisPosition);
        }
        
        public FormationPosition CreateFormationPositionForSquadNumber(int squadNumber, Vector3 colliderBounds, Vector3 robotPrefabSize, float robotYAxisPosition)
        {
            return new FormationPosition()
            {
                Number = squadNumber,
                CalculatedPosition = CalculateFormationPositionForSquadNumber(squadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition)
            };
        }

        public Vector3 CalculateFormationPositionForSquadNumber(int squadNumber, Vector3 colliderBounds, Vector3 robotPrefabSize, float robotYAxisPosition)
        {
            switch (squadNumber)
            {
                case 1:
                    return new Vector3(colliderBounds.x - robotPrefabSize.x, robotYAxisPosition, 0);
                case 2:
                    return new Vector3(robotPrefabSize.x / 2, robotYAxisPosition, colliderBounds.z / 2 - robotPrefabSize.x / 2);
                case 3:
                    return new Vector3(robotPrefabSize.x / 2, robotYAxisPosition, -(colliderBounds.z / 2 - robotPrefabSize.x / 2));
                default:
                    return Vector3.zero;
            }
        }
        
        public Vector3 FindFormationPositionVectorBySquadNumber(IList<FormationPosition> formationPositions, int squadNumber)
        {
            return formationPositions.First(x => x.Number == squadNumber).CalculatedPosition;
        }
        
        public RobotPlanner AddPlannerComponentToRobot(TeamPosition teamPosition, GameObject robotObject)
        {
            return null;
//            switch (teamPosition)
//            {
//                case TeamPosition.Keeper:
//                    return robotObject.AddComponent<KeeperPlannerBehaviour>();
//                case TeamPosition.Midfielder:
//                    return robotObject.AddComponent<MidfielderPlannerBehaviour>();
//                case TeamPosition.Defender:
//                    return robotObject.AddComponent<DefenderPlannerBehaviour>();
//                case TeamPosition.Attacker:
//                    return robotObject.AddComponent<AttackerPlannerBehaviour>();
//                default:
//                    return null;
//            }
        }
        
        public string GetGoalName()
        {
            return _teamSide == TeamSide.Home ? Settings.HomeGoalLine : Settings.AwayGoalLine;
        }
    }
}
