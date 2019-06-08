using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using NUnit.Framework;
using Planners;
using UnityEngine;
using Team = PhysReps.Team;

namespace Tests
{
    public class TeamTests
    {
        [Test]
        public void AddKeeperPlannerComponentToRobot_ValidTeamPosition_ComponentAdded()
        {
            var team = new Team();
            var robotObject = new GameObject("robot");

            team.AddPlannerComponentToRobot(TeamPosition.Keeper, robotObject);
            
            Assert.IsTrue(robotObject.GetComponent<KeeperPlanner>() != null);
        }
        
        [Test]
        public void AddAttackerPlannerComponentToRobot_ValidTeamPosition_ComponentAdded()
        {
            var team = new Team();
            var robotObject = new GameObject("robot");

            team.AddPlannerComponentToRobot(TeamPosition.Attacker, robotObject);
            
            Assert.IsTrue(robotObject.GetComponent<AttackerPlanner>() != null);
        }
        
        [Test]
        public void AddMidfielderPlannerComponentToRobot_ValidTeamPosition_ComponentAdded()
        {
            var team = new Team();
            var robotObject = new GameObject("robot");

            team.AddPlannerComponentToRobot(TeamPosition.Midfielder, robotObject);
            
            Assert.IsTrue(robotObject.GetComponent<MidfielderPlanner>() != null);
        }
        
        [Test]
        public void AddDefenderPlannerComponentToRobot_ValidTeamPosition_ComponentAdded()
        {
            var team = new Team();
            var robotObject = new GameObject("robot");

            team.AddPlannerComponentToRobot(TeamPosition.Defender, robotObject);
            
            Assert.IsTrue(robotObject.GetComponent<DefenderPlanner>() != null);
        }
        
        [Test]
        public void AddDefenderPlannerComponentToRobot_InValidTeamPosition_ComponentNotAdded()
        {
            var team = new Team();
            var robotObject = new GameObject("robot");

            team.AddPlannerComponentToRobot(TeamPosition.Midfielder, robotObject);
            
            Assert.IsTrue(robotObject.GetComponent<DefenderPlanner>() == null);
        }
        
        [Test]
        public void GetFormationPositions_TwoPositions_ListPopulated()
        {
            var team = new Team();
            var maxSquadNumber = 2;
            var colliderBounds = Vector3.zero;
            var robotPrefabSize = Vector3.zero;
            var robotYAxisPosition = 0.4f;

            var formationPositions = team.GetFormationPositions(maxSquadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition);
            
            Assert.IsTrue(formationPositions.ToList().Count == maxSquadNumber);
        }
        
        [Test]
        public void GetFormationPositions_ZeroPositions_ListNotPopulated()
        {
            var team = new Team();
            var maxSquadNumber = 0;
            var colliderBounds = Vector3.zero;
            var robotPrefabSize = Vector3.zero;
            var robotYAxisPosition = 0.4f;

            var formationPositions = team.GetFormationPositions(maxSquadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition);
            
            Assert.IsTrue(formationPositions.ToList().Count == maxSquadNumber);
        }
        
        [Test]
        public void GetFormationPositionForSquadNumber_ValidParameters_FormationPositionCreated()
        {
            var team = new Team();
            var squadNumber = 2;
            var colliderBounds = Vector3.zero;
            var robotPrefabSize = Vector3.zero;
            var robotYAxisPosition = 0.4f;

            var formationPosition = team.CreateFormationPositionForSquadNumber(squadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition);

            Assert.IsNotNull(formationPosition);
            Assert.IsTrue(formationPosition.Number == squadNumber);
        }
        
        [Test]
        public void FindFormationPositionVectorBySquadNumber_ValidSquadNumber_FormationPositionFound()
        {
            var team = new Team();
            var squadNumber = 2;
            
            var formationPositions = new List<FormationPosition>()
            {
                new FormationPosition()
                {
                    CalculatedPosition = Vector3.zero,
                    Number = squadNumber
                }
            };
            
            var formationPosition = team.FindFormationPositionVectorBySquadNumber(formationPositions, squadNumber);

            Assert.IsNotNull(formationPosition);
        }
        
        [Test]
        public void FindFormationPositionVectorBySquadNumber_InValidSquadNumber_FormationPositionNotFound()
        {
            var team = new Team();
            var squadNumber = 2;
            
            var formationPositions = new List<FormationPosition>()
            {
                new FormationPosition()
                {
                    CalculatedPosition = Vector3.zero,
                    Number = squadNumber
                }
            };

            Assert.Throws<InvalidOperationException>(() => team.FindFormationPositionVectorBySquadNumber(formationPositions, 3));
        }
        
        [Test]
        public void CalculateFormationPositionForSquadNumber_ValidSquadNumber_PositionReturned()
        {
            var team = new Team();
            var squadNumber = 2;
            var colliderBounds = Vector3.zero;
            var robotPrefabSize = Vector3.zero;
            var robotYAxisPosition = 0.4f;

            var formationPosition = team.CalculateFormationPositionForSquadNumber(squadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition);

            Assert.IsNotNull(formationPosition);
        }
    }
}
