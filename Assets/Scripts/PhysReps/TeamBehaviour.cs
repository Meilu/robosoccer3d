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
        public TeamSide teamSide;
        
        private IList<FormationPosition> _formationsPositions;
        private IList<Robot> _robots;
        private Team _team;
        
        private void Awake()
        {
            _team = new Team();
            _formationsPositions = _team.GetFormationPositions(3, GetComponent<BoxCollider>().bounds.size, robotPrefab.transform.GetComponent<BoxCollider>().size, -0.4f).ToList();
            
            // Create some robots (for now hardcoded, but in the future the player will be able to set these from the ui and add any robot he wishes :))
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
                var positionVector = _team.FindFormationPositionVectorBySquadNumber(_formationsPositions, robotModel.Number);
                var robot = Instantiate(robotPrefab, transform, false);
                robot.transform.localPosition = new Vector3(positionVector.x, positionVector.y, positionVector.z);
                RobotPlanner plannerComponent = _team.AddPlannerComponentToRobot(robotModel.TeamPosition, robot);
                
                // Save all of the properties of the robotmodel onto the prefab, so we can access them during the game if needed:)
                plannerComponent.RobotModel = robotModel;
            }
        }   
    }

    // Placed all logic in a seperate class so we can unit test it without annoying unity dependencies. (humble pattern)
    public class Team
    {
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
            switch (teamPosition)
            {
                case TeamPosition.Keeper:
                    return robotObject.AddComponent<KeeperPlanner>();
                case TeamPosition.Midfielder:
                    return robotObject.AddComponent<MidfielderPlanner>();
                case TeamPosition.Defender:
                    return robotObject.AddComponent<DefenderPlanner>();
                case TeamPosition.Attacker:
                    return robotObject.AddComponent<AttackerPlanner>();
                default:
                    return null;
            }
        }
        
    }
}
