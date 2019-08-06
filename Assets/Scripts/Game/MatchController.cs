using System;
using DataModels;

namespace Game
{
    public class MatchController : UnityEngine.MonoBehaviour
    {
        private Match _match;
        private int teamHomePlayerAmount;
        private int teamAwayPlayerAmount;

        private Color teamHomeOutfit;
        private Color teamAwayOutfit;
        
        public bool StartGame()
        {
            if (ThereAreEnoughPlayersOnTheTeam(teamHomePlayerAmount)
                && ThereAreEnoughPlayersOnTheTeam(teamAwayPlayerAmount)
                && !PlayerOutfitsAreTheSameOnOtherTeam(teamHomeOutfit, teamAwayOutfit))
            {

                return true;
            }
            else
                Console.Write("Start Game conditions are not met");
            return false;
        }
        
        public bool ThereAreEnoughPlayersOnTheTeam(int teamPlayerAmount)
        {
            return (teamPlayerAmount >= 7 && teamPlayerAmount <= 11);
        }

        public bool PlayerOutfitsAreTheSameOnOtherTeam(Color ColorTeamHome, Color ColorTeamAway)
        {
            return (ColorTeamHome == ColorTeamAway);
        }

        public void SetStartGameTime()
        {
            _match.StartTime = DateTime.Now;
        }

        public void SetFirstHalfTime()
        {
            
        }
        
        public void SetSecondHalfTime()
        {
            
        }
        
        public void SetFirstHalfExtraTime()
        {
            
        }
        
        public void SetSecondHalfExtraTime()
        {
            
        }

        public void StartPenalties()
        {
            
        }
        
    }
}