using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysReps;

namespace EventSystem.Handlers
{
    public class SoccerRulesHandlerBehaviour : MonoBehaviour
    {
        private SoccerRulesHandler _rulesHandler;
        // Start is called before the first frame update
        void Start()
        {
            var canStart = _rulesHandler.CheckForRulesToStartGame();

            if (!canStart)
                // toon melding
                return;
            
            // start game
        }

        // Update is called once per frame
        void Update()
        {
            if (_rulesHandler.GameHasEnded)
                return;
            
            _rulesHandler.CheckForRulesDuringTheGame();
            
        }
    }

    public class SoccerRulesHandler
    {
        private int teamHomePlayerAmount;
        private int teamAwayPlayerAmount;

        private Color teamHomeOutfit;
        private Color teamAwayOutfit;

        Vector3 ballPosition = new Vector3 (0f, 0f, 0f);
        Team teamToShoot;

        private Team teamHome;
        private Team teamAway;

        //these bools should come from events during the game.
        public bool GameHasEnded = false;
        public bool ballCrossedTheLine = false;
        public bool gameStateChanged = false;
        public bool foulHasBeenCommitted = false;

        public bool CheckForRulesToStartGame()
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

        public void CheckForRulesDuringTheGame()
        {
            //checks for 3 different reasons why the ball should be replaced and a team gets to shoot
            if (ballCrossedTheLine || gameStateChanged || foulHasBeenCommitted)
            {
                ballPosition = ReturnNewBallLocation(ballPosition);
                teamToShoot = ReturnTeamToShoot(teamHome, teamAway);
            }
            //no rules implemented yet for changing players during the game
        }

        public bool ThereAreEnoughPlayersOnTheTeam(int teamPlayerAmount)
        {
            return (teamPlayerAmount >= 7 && teamPlayerAmount <= 11);
        }

        public bool PlayerOutfitsAreTheSameOnOtherTeam(Color ColorTeamHome, Color ColorTeamAway)
        {
            return (ColorTeamHome == ColorTeamAway);
        }  

        public Vector3 ReturnNewBallLocation(Vector3 ballLocation)
        {
            //depending on the event, the ball will get a different location. For now just this position
            return new Vector3(0, 0, 0);
        }

        public Team ReturnTeamToShoot(Team teamHome, Team teamAway)
        {
            //depending on the event, should determine which team gets to shoot. For now just team home
            return teamHome;
        }
    }
}