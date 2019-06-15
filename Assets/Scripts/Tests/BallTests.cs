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

        // [SetUp] Zorgt ervoor dat die deze functie uitvoert voordat elk testje wordt uitgevoerd. dit is handig als we data willen klaarzetten die over testjes gedeeld wordt in dit bestand, zoals het ball object.
        [SetUp]
        public void Init()
        {
            // elke keer als een testje wordt uitgevoerd maakt die een nieuw ball object aan. deze heb je nodig om de logica te kunnen testen. hierin zitten de functies 
            _ball = new Ball();
        }

        // Door [Test] boven een functie te zetten en dit bestandje in de tests folder te plaatsen weet unity dat dit een unit test is.
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
            float ShootingForce = _ball.ShootingForce;

            Vector3 robotPosition = new Vector3(0.0f, 1.2f, 0.0f);
            Vector3 ballPosition = new Vector3(0.0f, 1.0f, 0.0f);

            var direction = _ball.GetBallShootingDirectionWithForce(robotPosition, ballPosition);
            Assert.IsTrue(direction == -(robotPosition - ballPosition).normalized * ShootingForce);
        }

    }
}