using System;
using System.Collections;
using System.Collections.Generic;
using DataModels;
using EventSystem.Events;
using Game;
using UnityEngine;
using Team = PhysReps.Team;

namespace EventSystem.Handlers
{
    public class SoccerRulesHandlerBehaviour : MonoBehaviour
    {
        private SoccerRulesHandler _rulesHandler;
        // Start is called before the first frame update
        void Start()
        {
            _rulesHandler.InitializeMatch();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_rulesHandler.GameHasStarted)
                return;
            
            
            
            _rulesHandler.CheckForRulesDuringTheGame();            
        }
    }

    public class SoccerRulesHandler
    {

        private MatchController _matchController;
        
        Vector3 ballPosition = new Vector3 (0f, 0f, 0f);

        //these bools should come from events during the game.
        public bool ballCrossedTheLine = false;
        public bool gameStateChanged = false;
        public bool foulHasBeenCommitted = false;

        public bool GameHasStarted = false;
        public bool CheckForRulesToStartGame()
        {
            return;
        }

        public void InitializeMatch()
        {
            // Create the teams (they need to come from the unity scene but creating them here for now as an example)
            var homeTeam = new DataModels.Team();
            var awayTeam = new DataModels.Team();
            
            // Create the match
            var match = _matchController.CreateMatch(homeTeam, awayTeam);
            
            // Validate if it can be started at all
            var canBeStarted = _matchController.ValidateStartMatch(match);

            if (!canBeStarted)
            {
                // toon melding i geuss
                return;
            }
            
            // The match can be started, set the starttime
            _matchController.SetStartGameTime(match);
            
            // Finally, raise an event that the match has started along with the match itself.
            EventManager.Instance.Raise(
                new StartMatchEvent(
                    match
                ));
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

        public void GiveExtraTimeToTheMatch()
        {
            
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