using System;
using System.Collections;
using System.Collections.Generic;
using DataModels;
using EventSystem;
using EventSystem.Events;
using EventSystem.UIHandlers;
using UnityEngine;

namespace Game
{
    public class RefereeControllerBehaviour : MonoBehaviour
    {
        private RefereeController _refereeController;
        public MatchControllerBehaviour matchControllerBehaviour;
        public TimerBehaviour matchTimer;
        private void Start()
        {
            _refereeController = new RefereeController();
            print("Adding referee listener");
            EventManager.Instance.AddListener<MatchTimerStartedEvent>(MatchTimerStartedEventLister);
        }

        void MatchTimerStartedEventLister(MatchTimerStartedEvent e)
        {
            // Now that the timer has started, we should create the match object and then start playing the actual match.
            var match = CreateMatch();

            if (!_refereeController.ValidateStartMatch(match))
            {
                // Match is not allowed to start, show warning?
                return;
            }
            
            // All good, throw the startmatchevent so that the matchcontroller can take over.
            EventManager.Instance.Raise(new StartMatchEvent(match));
        }

        private Match CreateMatch()
        {
            // Let the referee define the data for the teams for now. they will come from the players in the future
            var homeTeamModel = new Team()
            {
                Name = "Alpha"
            };
            
            var awayTeamModel = new Team()
            {
                Name = "Beta"
            };
            
            // Let the referee also be the creator of the match for now, just like the teams, in the future this wil come from a game manager or something.
            var match = new Match(homeTeamModel, awayTeamModel)
            {
                // Generate an id?
                Id = 1
            };

            return match;
        }
        // Update is called once per frame
        void Update()
        {
            if (!_refereeController.GameHasStarted)
                return;
            
            _refereeController.CheckForRulesDuringTheGame();            
        }
    }

    public class RefereeController
    {
        
        Vector3 ballPosition = new Vector3 (0f, 0f, 0f);

        //these bools should come from events during the game.
        public bool ballCrossedTheLine = false;
        public bool gameStateChanged = false;
        public bool foulHasBeenCommitted = false;

        public bool GameHasStarted = false;

        public bool ValidateStartMatch(Match match)
        {
            return ThereAreEnoughPlayersOnTheTeam(match.HomeTeam.GetPlayerCount())
                   && ThereAreEnoughPlayersOnTheTeam(match.AwayTeam.GetPlayerCount())
                   && !PlayerOutfitsAreTheSameOnOtherTeam(match.HomeTeam.TeamColor, match.AwayTeam.TeamColor);
        }
        
        public bool PlayerOutfitsAreTheSameOnOtherTeam(Color ColorTeamHome, Color ColorTeamAway)
        {
            return false;
            return (ColorTeamHome == ColorTeamAway);
        }

        public bool ThereAreEnoughPlayersOnTheTeam(int teamPlayerAmount)
        {
            return true;
            return (teamPlayerAmount >= 7 && teamPlayerAmount <= 11);
        }
        
        public void CheckForRulesDuringTheGame()
        {
            //checks for 3 different reasons why the ball should be replaced and a team gets to shoot
            if (ballCrossedTheLine || gameStateChanged || foulHasBeenCommitted)
            {
                ballPosition = ReturnNewBallLocation(ballPosition);
                //teamToShoot = ReturnTeamToShoot(teamHome, teamAway);
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