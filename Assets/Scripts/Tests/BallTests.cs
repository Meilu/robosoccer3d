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
            // Hier kan je de isHitByFrontLegs functie testen van het ball object. (_ball.isHitByFrontLegs())
            
            // met Assert.IsTrue(resultaathier) verwacht die dat de functie true gaat returnen, dus hoe kan je de IsHitByFrontLegs functie uitvoeren zodat die true returned?
            // kijk ook in team.cs voor meer voorbeelden
        }
        
        // TODO: ook een functie maken om het false pad van de ishitbyfrontlegs functie te testen, zelfde testie als hierboven maar hoe ga je hem false laten returnen en false asserten? 
    }
}