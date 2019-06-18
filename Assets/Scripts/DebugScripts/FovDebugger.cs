using Sensors;
using UnityEditor;
using UnityEngine;

namespace DebugScripts
{

    public class FovDebugger : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            DrawFieldOfView();
        }
        
        private void DrawFieldOfView()
        {
            var robotVisionSensor = GetComponent<RobotVisionSensorBehaviour>();
        
            float rayRange = 10.0f;
            float halfFOV = robotVisionSensor.viewAngle;
        
            Quaternion leftRayRotation = Quaternion.AngleAxis( -halfFOV, Vector3.up );
            Quaternion rightRayRotation = Quaternion.AngleAxis( halfFOV, Vector3.up );
    
            var transform = robotVisionSensor.transform;
            var forward = transform.forward;
        
            Vector3 leftRayDirection = leftRayRotation * forward;
            Vector3 rightRayDirection = rightRayRotation * forward;
            Gizmos.DrawRay( transform.position, leftRayDirection * rayRange );
            Gizmos.DrawRay( robotVisionSensor.transform.position, rightRayDirection * rayRange );
        }
    }
}
