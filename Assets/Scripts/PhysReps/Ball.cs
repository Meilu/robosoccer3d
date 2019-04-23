using UnityEngine;
using UnityEngine.Events;

namespace PhysReps
{
    public class Ball : MonoBehaviour
    {
        private UnityAction ballOutListener;
        private Vector3 originalPosition;
        
        private void Awake()
        {
            ballOutListener = new UnityAction(ResetBall);
            originalPosition = transform.position;
        }

        private void Update()
        {
           // print(GetComponent<Rigidbody2D>().velocity.x);
            // transform.Find("Sphere").GetComponent<Rigidbody>().AddTorque(0, -GetComponent<Rigidbody2D>().velocity.x, 0, ForceMode.VelocityChange);
        }

        void OnEnable()
        {
            // Attach event listeners to the ballout and homegoal events. 
            // Both will trigger the ballOutListener, which is bound to the resetBall function
            EventManager.Instance.StartListeningToEvent(EventType.BallOut, ballOutListener);
            EventManager.Instance.StartListeningToEvent(EventType.HomeGoal, ballOutListener);
        }

        void OnDisable()
        {
            // EventManager.Instance.StopListeningToEvent(EventType.BallOut, ballOutListener);
        }
        /// <summary>
        /// This function will reset the ball to its original position and remove any velocity added by addForce.
        /// </summary>
        void ResetBall()
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = originalPosition;
        }
    }
}