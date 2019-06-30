using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem.Handlers
{
    public class SoccerRulesHandlerBehaviour : MonoBehaviour
    {
        private SoccerRulesHandler _rulesHandler;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_rulesHandler.CheckForRulesToStartGame()) {
                do
                {
                    _rulesHandler.CheckForRulesDuringTheGame();
                }
                while (!_rulesHandler.GameHasEnded());
            };
        }
    }

    public class SoccerRulesHandler
    {
        private int teamHomePlayerAmount;
        private int teamAwayPlayerAmount;

        private Color teamHomeOutfit;
        private Color teamAwayOutfit;

        public bool CheckForRulesToStartGame()
        {
            if (ThereAreEnoughPlayersOnTheTeam(teamHomePlayerAmount) 
                && ThereAreEnoughPlayersOnTheTeam(teamAwayPlayerAmount) 
                && PlayerOutfitsAreNotTheSameOnOtherTeam(teamHomeOutfit, teamAwayOutfit))
            {
                return true;
            } else return false;
        }

        public void CheckForRulesDuringTheGame()
        {
            Console.Write("checking rules during the game");
        }

        public bool ThereAreEnoughPlayersOnTheTeam(int teamPlayerAmount)
        {
            if (teamPlayerAmount >= 7 && teamPlayerAmount <= 11)
            {
                return true;
            }
            else return false;
        }

        public bool PlayerOutfitsAreNotTheSameOnOtherTeam(Color ColorTeamHome, Color ColorTeamAway)
        {
            if (ColorTeamHome == ColorTeamAway)
            {
                return false;
            } else return true;
        }

        public bool GameHasEnded()
        {
            return false;
        }
    }
}