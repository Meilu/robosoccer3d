using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Sensors
{
    public class RobotVisionSensor : MonoBehaviour
    {
        private GameObject _ballObject;
        private int _objectsLayerMask; 
        
        public float viewRadius;
        [Range(0,360)]
        public float viewAngle;
        public float ballWithinRange;
        public float meshResolution;
        
        public UnityEvent ballInsideVision;
        public UnityEvent ballNotInsideVision;
        public UnityEvent ballInsideShootingRange;
        public UnityEvent ballNotInsideShootingRange;
        
        public UnityEvent goalInsideVision;
        public UnityEvent goalNotInsideVision;
        public UnityEvent goalInsideShootingRange;
        public UnityEvent goalNotInsideShootingRange;

        [HideInInspector]
        public List<Transform> visibleTargets = new List<Transform>();
    
        public RobotVisionSensor()
        {
            ballInsideVision = new UnityEvent();
            ballNotInsideVision = new UnityEvent();
            ballInsideShootingRange = new UnityEvent();
            ballNotInsideShootingRange = new UnityEvent();

            goalInsideVision = new UnityEvent();
            goalNotInsideVision = new UnityEvent();
            goalInsideShootingRange = new UnityEvent();
            goalNotInsideShootingRange = new UnityEvent();
        }

        void Start() {
            _objectsLayerMask = 1 << LayerMask.NameToLayer("objects");; 
        }

        private void FixedUpdate()
        {
            FindVisibleTargets(); 
        }

        IEnumerator FindTargetsWithDelay(float delay) {
            while (true) {
                yield return new WaitForSeconds (delay);
                FindVisibleTargets ();
            }
        }

        void FindVisibleTargets() {
          //  visibleTargets.Clear ();

            var position = transform.parent.position;
//            Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(position, viewRadius, _objectsLayerMask);
//
//            var poi = targetsInViewRadius.FirstOrDefault(x => x.transform.name == "soccerball" || x.transform.name == "leftGoalLine");

            var soccerball = GameObject.Find("soccerball");
            var leftGoalLine = GameObject.Find("leftGoalLine");
            
            if (!soccerball || !leftGoalLine)
                return;
        
            Vector2 dirToSoccerball = (soccerball.transform.position - position).normalized;
            Vector2 dirToLeftGoalLine = (leftGoalLine.transform.position - position).normalized;


            if (Vector2.Angle(transform.up, dirToSoccerball) < viewAngle / 2)
            {
                print(" ball in angle");
                var distanceToBall = Vector2.Distance(soccerball.transform.position, position);

                if (distanceToBall < 0.5)
                {
                    ballInsideShootingRange.Invoke();
                }
                else
                {
                    ballInsideVision.Invoke();
                    ballNotInsideShootingRange.Invoke();
                }
            }
            else
            {
                ballNotInsideVision.Invoke();
            }
            
            if (Vector2.Angle(transform.up, dirToLeftGoalLine) < viewAngle)
            {
                var distanceToGoal = Vector2.Distance(leftGoalLine.transform.position, position);

                if (distanceToGoal < 0.5)
                {
                    goalInsideShootingRange.Invoke();
                }
                else
                {
                    goalInsideVision.Invoke();
                    goalNotInsideShootingRange.Invoke();
                }
            }
            else
            {
                goalNotInsideVision.Invoke();
            }

        }
        private void OnDrawGizmos()
        {
            DrawFieldOfView();
        }
        private void DrawFieldOfView()
        {
            float totalFOV = viewRadius;
            float rayRange = 10.0f;
            float halfFOV = viewAngle / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis( -halfFOV, Vector3.back );
            Quaternion rightRayRotation = Quaternion.AngleAxis( halfFOV, Vector3.back );
            Vector3 leftRayDirection = leftRayRotation * transform.up;
            Vector3 rightRayDirection = rightRayRotation * transform.up;
            Gizmos.DrawRay( transform.position, leftRayDirection * rayRange );
            Gizmos.DrawRay( transform.position, rightRayDirection * rayRange );
        }
        
        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.parent.eulerAngles.z;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    
    }
}
