using UnityEngine;

namespace Sensors
{
    public class RobotTravelDistanceSensorBehaviour : MonoBehaviour
    {
        private Vector3 _lastPosition;
        public float distanceTravelled { get; set; }

        private void FixedUpdate()
        {
            var position = transform.position;
            
            distanceTravelled += Vector3.Distance(position, _lastPosition);
            _lastPosition = position;
        }
    }
}
