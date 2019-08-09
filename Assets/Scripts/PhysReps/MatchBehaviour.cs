using DataModels;
using UnityEngine;

namespace PhysReps
{
    public class MatchBehaviour : MonoBehaviour
    {
        public Match Match { get; set; }
        public int Duration { get; set; }

        private void Update()
        {
            // if match is started update duration every n ms
        }
    }
}