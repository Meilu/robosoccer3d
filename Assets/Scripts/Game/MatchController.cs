using System;
using DataModels;
using UnityEngine;

namespace Game
{
    public class MatchController
    {

        public Match CreateMatch(Team homeTeam, Team awayTeam)
        {
            var match = new Match(homeTeam, awayTeam)
            {
                // Generate an id?
                Id = 1
            };

            // Set any initial properties for a match here

            // Return it
            return match;
        }
        public bool ValidateStartMatch(Match match)
        {
            return ThereAreEnoughPlayersOnTheTeam(match.HomeTeam.GetPlayerCount())
                   && ThereAreEnoughPlayersOnTheTeam(match.AwayTeam.GetPlayerCount())
                   && !PlayerOutfitsAreTheSameOnOtherTeam(match.HomeTeam.TeamColor, match.AwayTeam.TeamColor);

        }
        
        public bool ThereAreEnoughPlayersOnTheTeam(int teamPlayerAmount)
        {
            return (teamPlayerAmount >= 7 && teamPlayerAmount <= 11);
        }

        public bool PlayerOutfitsAreTheSameOnOtherTeam(Color ColorTeamHome, Color ColorTeamAway)
        {
            return (ColorTeamHome == ColorTeamAway);
        }

        public void SetStartGameTime(Match match)
        {
            match.StartTime = DateTime.Now;
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