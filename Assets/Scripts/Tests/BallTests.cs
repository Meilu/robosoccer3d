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
            // Hier kan je de isHitByFrontLegs functie testen van het ball object: _ball.isHitByFrontLegs(naamvanlegshier)
            // met Assert.IsTrue(resultaathier) verwacht die dat de functie true gaat returnen, dus hoe kan je de IsHitByFrontLegs functie uitvoeren zodat die true returned?
            // kijk ook in team.cs voor meer voorbeelden
        }
        
        // TODO: ook een functie maken om het false pad van de ishitbyfrontlegs functie te testen, zelfde testie als hierboven maar hoe ga je hem false laten returnen en false asserten? 
    }
}