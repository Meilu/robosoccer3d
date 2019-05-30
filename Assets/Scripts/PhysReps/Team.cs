﻿using System.Collections.Generic;
using System.Linq;
using DataModels;
using Planners;
using UnityEngine;

namespace PhysReps
{
    
    public class Team : MonoBehaviour
    {
        private IList<Robot> _robots;
        public GameObject robotPrefab;
        public TeamSide teamSide;
        private IList<FormationPosition> _formationsPositions;
        
        private void Awake()
        {
            InitializeFormationPositions();
            
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
                // Get the position that belongs to this robot his squad number from the formationpositionslist
                var positionVector = _formationsPositions.First(x => x.Number == robotModel.Number).CalculatedPosition;
                
                var robot = Instantiate(robotPrefab, transform, false);
                
                // Place the robot on the correct position based on the positionvector from the formationpositions list.
                robot.transform.localPosition = new Vector3(positionVector.x, positionVector.y, positionVector.z);
                
                // Now, i think this is what you wanted... Here we can now save the position of the robotprefab based on the property of the model.
                // Because i restructured the planners and made a different planner for every teamposition type, planners now need to be added dynamically here.
                // And because each planner already is of a specific type there maybe there is no more need to save the position also. unless we want to offcourse it is possible. 
                // If you do see a further purpose for it i can add it if you want.

                RobotPlanner plannerComponent = AddPlannerComponent(robotModel, robot);
                
                // Save all of the properties of the robotmodel onto the prefab, so we can access them during the game if needed:)
                plannerComponent.RobotModel = robotModel;
            }
        }

        private RobotPlanner AddPlannerComponent(Robot robotModel, GameObject robotObject)
        {
            switch (robotModel.TeamPosition)
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
        
        /// <summary>
        /// Calculate all positions for every formation we support, based on the size of our scene and our prefabs.
        /// </summary>
        private void InitializeFormationPositions()
        {   
            // Get the bounds of the team area. We can calculate positions from these bounds so that they are not screensize dependent.
            // If we want to have them on a specific side of the field we can simply flip this team object and they wil be on the other side. really easy.
            var colliderBounds = GetComponent<BoxCollider>().bounds.size;
            var robotPrefabSize = robotPrefab.transform.GetComponent<BoxCollider>().size;
            var robotYAxisPosition = -0.4f;
           
            _formationsPositions = new List<FormationPosition>()
            {
                new FormationPosition()
                {
                    Number = 1,
                    CalculatedPosition = new Vector3(colliderBounds.x - robotPrefabSize.x, robotYAxisPosition, 0)
                },
                new FormationPosition()
                {
                    Number = 2,
                    CalculatedPosition = new Vector3(robotPrefabSize.x / 2, robotYAxisPosition, colliderBounds.z / 2 - robotPrefabSize.x / 2)
                },
                new FormationPosition()
                {
                    Number = 3,
                    CalculatedPosition =  new Vector3(robotPrefabSize.x / 2, robotYAxisPosition,   -(colliderBounds.z / 2 - robotPrefabSize.x / 2))
                }
            };
        }
    }
}
