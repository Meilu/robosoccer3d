using System;
using UnityEngine;
using UnityEngine.Events;

namespace PhysReps
{
    public class BallBehaviour : MonoBehaviour
    {
        private Ball _ball;

        private void Awake()
        {
            _ball = new Ball();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_ball.IsHitByFrontLegs(other.transform.name))
                return;

            var direction = _ball.GetBallShootingDirectionWithForce(other.transform.position, transform.position);
            
            GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        }
    }

    public class Ball
    {
        public float ShootingForce = 0.8f;
        
        public bool IsHitByFrontLegs(string colliderName)
        {
            return colliderName == "frontLegs";
        }

        public Vector3 GetBallShootingDirectionWithForce(Vector3 robotPosition, Vector3 ballPosition)
        {
            return -(robotPosition - ballPosition).normalized * ShootingForce;
        }
    }
}