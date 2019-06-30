using NUnit.Framework;
using PhysReps;
using UnityEngine;
using EventSystem.Handlers;

namespace Tests
{
    [TestFixture]
    public class SoccerRulesHandlerTests
    {
        private SoccerRulesHandler _soccerRulesHandler;

        [SetUp]
        public void Init()
        {
            _soccerRulesHandler = new SoccerRulesHandler();
        }

        [Test]
        public void CheckForRulesToStartGame_ReturnsTrue()
        {
           // _soccerRulesHandler.ThereAreEnoughPlayersOnTheTeam() = true;
           // _soccerRulesHandler.playerOutfitsAreNotTheSameOnOtherTeam() = true;

            Assert.IsTrue(_soccerRulesHandler.CheckForRulesToStartGame());
        }

        [Test]
        public void ThereAreEnoughPLayersOnTheTeams_ReturnsTrue()
        {
            int teamPlayerAmount = 11;
            Assert.IsTrue(_soccerRulesHandler.ThereAreEnoughPlayersOnTheTeam(teamPlayerAmount));
        }

        [Test]
        public void ThereAreTooFewPLayersOnTheTeam_ReturnsFalse()
        {
            int teamPlayerAmount = 6;
            Assert.IsFalse(_soccerRulesHandler.ThereAreEnoughPlayersOnTheTeam(teamPlayerAmount));
        }

        [Test]
        public void ThereAreTooManyPLayersOnTheTeam_ReturnsFalse()
        {
            int teamPlayerAmount = 12;
            Assert.IsFalse(_soccerRulesHandler.ThereAreEnoughPlayersOnTheTeam(teamPlayerAmount));
        }

    }
}