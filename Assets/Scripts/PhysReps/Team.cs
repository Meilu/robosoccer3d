using System.Collections.Generic;
using UnityEngine;

namespace PhysReps
{
    
    public class Team : MonoBehaviour
    {
        private IList<GameObject> players;
        public GameObject robotPrefab;
        public TeamSide teamSide; 
        public Sprite RobotSprite;
        
        enum Formation
        {
            Offensive,
            Defensive,
            Neutral
        }

        private Dictionary<Formation, Dictionary<int, Vector2>> _formationsPositionsDictionary;
        
        private void Awake()
        {
            // Get the bounds of the team area. We can calculate positions from these bounds so that they are not screensize dependent.
            // If we want to have them on a specific side of the field we can simply flip this team object and they wil be on the other side. really easy.
            var colliderBounds = GetComponent<Collider2D>().bounds.size;

           var robotPrefabSize = robotPrefab.gameObject.transform.Find("robot").Find("collider").GetComponent<BoxCollider2D>().size;

            _formationsPositionsDictionary = new Dictionary<Formation, Dictionary<int, Vector2>>
            {
                {
                    Formation.Offensive, new Dictionary<int, Vector2>
                    {
                        {1, new Vector2(colliderBounds.x - robotPrefabSize.x, 0)},
                        {2, new Vector2(0, (colliderBounds.y / 2 - robotPrefabSize.y / 2) * -1)}, 
                        {3, new Vector2(0, (colliderBounds.y / 2 - robotPrefabSize.y / 2))} 
                    }
                }
            };

            var teamIsFlipped = transform.localScale.x < 0;
            foreach (KeyValuePair<Formation, Dictionary<int, Vector2>> positions in _formationsPositionsDictionary)
            {
                foreach (KeyValuePair<int, Vector2> positionVector in positions.Value)
                {
                    var robot = Instantiate(robotPrefab, transform, false);
                    
                    // If this team's x scale is flipped (used for the opposite side) we also have to flip the text for this robot, else it's in mirror.
                    if (teamIsFlipped)
                        FlipModeCanvasRect(robot);

                    robot.transform.localPosition = new Vector2(positionVector.Value.x, positionVector.Value.y);
                    return;
                }
            }
        }

        private void FlipModeCanvasRect(GameObject robot)
        {
            var rectTransform = robot.transform.Find("modeCanvas").GetComponent<RectTransform>();
            var localScale = rectTransform.localScale;
            rectTransform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }

        private void AssignSpriteToRobot(GameObject robot)
        {
            
        }
    }
}
