using NUnit.Framework;
using PhysReps;
using UnityEngine;
using Ball = PhysReps.Ball;

namespace Tests
{
    [TestFixture]
    public class BallTests
    {
        private Ball _ball;

        [SetUp]
        public void Init()
        {
            _ball = new Ball();
        }

        [Test]
        public void IsHitByFrontLegs_ColliderIsFrontLegs_ReturnsTrue()
        {
            var colliderName = "frontLegs";

            _ball.IsHitByFrontLegs(colliderName);
            Assert.IsTrue(_ball.IsHitByFrontLegs(colliderName));
        }

        [Test]
        public void IsHitByFrontLegs_ColliderIsNotFrontLegs_ReturnsTrue()
        {
            var colliderName = "frontArms";

            _ball.IsHitByFrontLegs(colliderName);
            Assert.IsTrue(!_ball.IsHitByFrontLegs(colliderName));
        }

        [Test]
        public void GetBallShootingDirectionWithForce__ReturnsCorrectDirection()
        {
            float shootingForce = _ball.ShootingForce;

            Vector3 robotPosition = new Vector3(0.0f, 1.2f, 0.0f);
            Vector3 ballPosition = new Vector3(0.0f, 1.0f, 0.0f);

            var direction = _ball.GetBallShootingDirectionWithForce(robotPosition, ballPosition);
            Assert.IsTrue(direction == -(robotPosition - ballPosition).normalized * shootingForce);
        }

    }
}