//using NUnit.Framework;
//using PhysReps;
//using UnityEngine;
//using Game;
//
//namespace Tests
//{
//    [TestFixture]
//    public class MatchControllerTests
//    {
//        private MatchController _matchController;
//
//        [SetUp]
//        public void Init()
//        {
//            _matchController = new MatchController();
//        }
//
//        [Test]
//        public void ThereAreEnoughPLayersOnTheTeams_ReturnsTrue()
//        {
//            int teamPlayerAmount = 11;
//            Assert.IsTrue(_matchController.ThereAreEnoughPlayersOnTheTeam(teamPlayerAmount));
//        }
//
//        [Test]
//        public void ThereAreTooFewPLayersOnTheTeam_ReturnsFalse()
//        {
//            int teamPlayerAmount = 6;
//            Assert.IsFalse(_matchController.ThereAreEnoughPlayersOnTheTeam(teamPlayerAmount));
//        }
//
//        [Test]
//        public void ThereAreTooManyPLayersOnTheTeam_ReturnsFalse()
//        {
//            int teamPlayerAmount = 12;
//            Assert.IsFalse(_matchController.ThereAreEnoughPlayersOnTheTeam(teamPlayerAmount));
//        }
//
//        [Test]
//        public void PlayerOutfitsAreTheSameOnOtherTeam_ReturnsTrue()
//        {
//            Color ColorTeamHome = Color.blue;
//            Color ColorTeamAway = Color.blue;
//
//            Assert.IsTrue(_matchController.PlayerOutfitsAreTheSameOnOtherTeam(ColorTeamHome, ColorTeamAway));
//        }
//
//        [Test]
//        public void PlayerOutfitsAreTheSameOnOtherTeam_ReturnsFalse()
//        {
//            Color ColorTeamHome = Color.blue;
//            Color ColorTeamAway = Color.green;
//
//            Assert.IsFalse(_matchController.PlayerOutfitsAreTheSameOnOtherTeam(ColorTeamHome, ColorTeamAway));
//        }
//
//    }
//}