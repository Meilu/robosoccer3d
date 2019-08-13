using System;
using UnityEngine;

namespace Sensors
{
    public class RobotTravelDistanceSensorBehaviour : MonoBehaviour
    {
        private Vector3 _lastPosition;
        private int nextActionTime = 10;

        public float distanceTraveled { get; set; }

        private void Start()
        {
            _lastPosition = transform.position;
        }

        void FixedUpdate()
        {
            var position = transform.position;
            
            distanceTraveled += Vector3.Distance(position, _lastPosition);
            _lastPosition = position;
            
            if (Time.time >= nextActionTime ) {
                nextActionTime = Mathf.FloorToInt(Time.time) + 10;
                print("distance travelled: " + distanceTraveled);
            }
            
        }
    }
}
