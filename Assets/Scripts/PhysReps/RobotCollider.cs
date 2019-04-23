using UnityEngine;
using UnityEngine.Events;

namespace PhysReps
{
    public class RobotCollider : MonoBehaviour
    {
        public float speed;
        private float time = 3.0f;

        private GameObject _ballObject;
        private Rigidbody2D _robotRigidBody2D;

        private bool _hasBall;
        void Start()
        {
            _ballObject = GameObject.FindWithTag("Ball");
            _robotRigidBody2D = transform.parent.GetComponent<Rigidbody2D>();
        }
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            // We only want to do something when we collided with the ball. ignore the other robots (for now).
            if (collision.name != "soccerball")
                return;
            
            // if collided with the ball, stop movement and set hasBall to true for this robot.
            _hasBall = true;
            _robotRigidBody2D.velocity = Vector2.zero;
            
           //  ShootBall(collision.gameObject);
        }

        /**
         * Will shoot the ball in the current direction
         */
        private void ShootBall(GameObject ball)
        {
            ball.transform.rotation = transform.rotation;
            ball.GetComponent<Rigidbody2D>().AddForce(ball.transform.up * 300);
        } 
        
        private void OnTriggerExit2D(Collider2D other)
        {
            _hasBall = false;
        }

        void FixedUpdate()
        {
            // Always keep looking at the ball.
            // LookAtBall();

            // if (!_hasBall)
            // Moves the robot towards the ball if he doesnt have it by applying force (after the lookAtBall function above)
            //_robotRigidBody2D.AddForce(transform.up * speed);
        }
        private void LookAtBall()
        {
            // Some hard math i don't understand that will cause the sprite to look at the ball.
            var parent = transform.parent;
            Vector3 diff = _ballObject.transform.position - parent.position;
 
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            parent.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        }
    }
}