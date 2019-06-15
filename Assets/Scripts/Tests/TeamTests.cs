using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using NUnit.Framework;
using Planners;
using UnityEditor;
using UnityEngine;
using Team = PhysReps.Team;

namespace Tests
{
    [TestFixture]
    public class TeamTests
    {
        private GameObject _robot;
        private Team _team;
        
        [SetUp]
        public void Init()
        {
            _team = new Team();
            _robot = new GameObject("robot");
        }
        [Test]
        public void AddPlannerComponentToRobot_ValidTeamPosition_KeeperComponentAdded()
        {
            _team.AddPlannerComponentToRobot(TeamPosition.Keeper, _robot);
            
            Assert.IsTrue(_robot.GetComponent<KeeperPlanner>() != null);
        }
        
        [Test]
        public void AddPlannerComponentToRobot_ValidTeamPosition_AttackerComponentAdded()
        {
            _team.AddPlannerComponentToRobot(TeamPosition.Attacker, _robot);
            
            Assert.IsTrue(_robot.GetComponent<AttackerPlanner>() != null);
        }
        
        [Test]
        public void AddPlannerComponentToRobot_ValidTeamPosition_MidfielderComponentAdded()
        {
            _team.AddPlannerComponentToRobot(TeamPosition.Midfielder, _robot);
            
            Assert.IsTrue(_robot.GetComponent<MidfielderPlanner>() != null);
        }
        
        [Test]
        public void AddPlannerComponentToRobot_ValidTeamPosition_DefenderComponentAdded()
        {
            _team.AddPlannerComponentToRobot(TeamPosition.Defender, _robot);
            
            Assert.IsTrue(_robot.GetComponent<DefenderPlanner>() != null);
        }
        
        [Test]
        public void AddPlannerComponentToRobot_InValidTeamPosition_DefenderComponentNotAdded()
        {
            _team.AddPlannerComponentToRobot(TeamPosition.Midfielder, _robot);
            
            Assert.IsTrue(_robot.GetComponent<DefenderPlanner>() == null);
        }
        
        [Test]
        public void GetFormationPositions_TwoPositions_ListPopulated()
        {
            var maxSquadNumber = 2;
            var colliderBounds = Vector3.zero;
            var robotPrefabSize = Vector3.zero;
            var robotYAxisPosition = 0.4f;

            var formationPositions = _team.GetFormationPositions(maxSquadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition);
            
            Assert.IsTrue(formationPositions.ToList().Count == maxSquadNumber);
        }
        
        [Test]
        public void GetFormationPositions_ZeroPositions_ListNotPopulated()
        {
            var maxSquadNumber = 0;
            var colliderBounds = Vector3.zero;
            var robotPrefabSize = Vector3.zero;
            var robotYAxisPosition = 0.4f;

            var formationPositions = _team.GetFormationPositions(maxSquadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition);
            
            Assert.IsTrue(formationPositions.ToList().Count == maxSquadNumber);
        }
        
        [Test]
        public void GetFormationPositionForSquadNumber_ValidParameters_FormationPositionCreated()
        {
            var squadNumber = 2;
            var colliderBounds = Vector3.zero;
            var robotPrefabSize = Vector3.zero;
            var robotYAxisPosition = 0.4f;

            var formationPosition = _team.CreateFormationPositionForSquadNumber(squadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition);

            Assert.IsNotNull(formationPosition);
            Assert.IsTrue(formationPosition.Number == squadNumber);
        }
        
        [Test]
        public void FindFormationPositionVectorBySquadNumber_ValidSquadNumber_FormationPositionFound()
        {
            var squadNumber = 2;
            
            var formationPositions = new List<FormationPosition>()
            {
                new FormationPosition()
                {
                    CalculatedPosition = Vector3.zero,
                    Number = squadNumber
                }
            };
            
            var formationPosition = _team.FindFormationPositionVectorBySquadNumber(formationPositions, squadNumber);

            Assert.IsNotNull(formationPosition);
        }
        
        [Test]
        public void FindFormationPositionVectorBySquadNumber_InValidSquadNumber_FormationPositionNotFound()
        {
            var squadNumber = 2;
            
            var formationPositions = new List<FormationPosition>()
            {
                new FormationPosition()
                {
                    CalculatedPosition = Vector3.zero,
                    Number = squadNumber
                }
            };

            Assert.Throws<InvalidOperationException>(() => _team.FindFormationPositionVectorBySquadNumber(formationPositions, 3));
        }
        
        [Test]
        public void CalculateFormationPositionForSquadNumber_ValidSquadNumber_PositionReturned()
        {
            var squadNumber = 2;
            var colliderBounds = Vector3.zero;
            var robotPrefabSize = Vector3.zero;
            var robotYAxisPosition = 0.4f;

            var formationPosition = _team.CalculateFormationPositionForSquadNumber(squadNumber, colliderBounds, robotPrefabSize, robotYAxisPosition);

            Assert.IsNotNull(formationPosition);
        }
    }
}
