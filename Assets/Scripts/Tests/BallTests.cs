using NUnit.Framework;
using PhysReps;
using Planners;
using UnityEngine;

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
            var isHit = _ball.IsHitByFrontLegs("frontLegs");
            
            Assert.IsTrue(isHit);
        }
        
        [Test]
        public void IsHitByFrontLegs_ColliderIsNotFrontLegs_ReturnsFalse()
        {
            var isHit = _ball.IsHitByFrontLegs("backlegs");
            
            Assert.IsFalse(isHit);
        }
        
        [Test]
        public void GetBallShootingDirectionWithForce_CurrentRobotPosition_CorrectDirectionCalculated()
        {
            var robotPosition = new Vector3()
            {
                x = 2,
                y = 2,
                z = 2
            };
            
            var ballPosition = new Vector3()
            {
                x = 1,
                y = 1,
                z = 1
            };

            _ball.ShootingForce = 0.2f;
            
            var shootingDirection = _ball.GetBallShootingDirectionWithForce(robotPosition, ballPosition);
            var expected = new Vector3(-0.115470052f, -0.115470052f, -0.115470052f);
            
            Assert.AreEqual(shootingDirection, expected);
        }
        
    }
}