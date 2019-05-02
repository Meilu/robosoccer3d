using System.Collections.Generic;
using UnityEngine;

namespace PhysReps
{
    
    public class Team : MonoBehaviour
    {
        private IList<GameObject> players;
        public GameObject robotPrefab;
        public TeamSide teamSide; 
        
        enum Formation
        {
            Offensive,
            Defensive,
            Neutral
        }

        private Dictionary<Formation, Dictionary<int, Vector3>> _formationsPositionsDictionary;
        
        private void Awake()
        {
            // Get the bounds of the team area. We can calculate positions from these bounds so that they are not screensize dependent.
            // If we want to have them on a specific side of the field we can simply flip this team object and they wil be on the other side. really easy.
            var colliderBounds = GetComponent<BoxCollider>().bounds.size;

           var robotPrefabSize = robotPrefab.transform.GetComponent<BoxCollider>().size;
           var robotYAxisPosition = -0.1f;
           
            _formationsPositionsDictionary = new Dictionary<Formation, Dictionary<int, Vector3>>
            {
                {

                    Formation.Offensive, new Dictionary<int, Vector3>
                    {
                        {1, new Vector3(colliderBounds.x - robotPrefabSize.x, robotYAxisPosition, 0)},
                        {2, new Vector3(robotPrefabSize.x / 2, robotYAxisPosition, colliderBounds.z / 2 - robotPrefabSize.x / 2)}, 
                        {3, new Vector3(robotPrefabSize.x / 2, robotYAxisPosition,   -(colliderBounds.z / 2 - robotPrefabSize.x / 2))}
                    }
                }
            };
            
//            _formationsPositionsDictionary = new Dictionary<Formation, Dictionary<int, Vector3>>
//            {
//                {
//
//                    Formation.Offensive, new Dictionary<int, Vector3>
//                    {
//                        {1, new Vector3(colliderBounds.x - robotPrefabSize.x, robotYAxisPosition, 0)}
//                    }
//                }
//            };
            
            foreach (KeyValuePair<Formation, Dictionary<int, Vector3>> positions in _formationsPositionsDictionary)
            {
                foreach (KeyValuePair<int, Vector3> positionVector in positions.Value)
                {
                    var robot = Instantiate(robotPrefab, transform, false);

                    robot.transform.localPosition = new Vector3(positionVector.Value.x, positionVector.Value.y, positionVector.Value.z);
                    return;
                }
            }
        }

        // If there is a canvas inside the robot, we need to reverse its scale for the awayteam because else it is flipped (doesnt matter for now but leaving this function here)
        private void FlipModeCanvasRect(GameObject robot)
        {
            var rectTransform = robot.transform.Find("modeCanvas").GetComponent<RectTransform>();
            var localScale = rectTransform.localScale;
            rectTransform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }
}
