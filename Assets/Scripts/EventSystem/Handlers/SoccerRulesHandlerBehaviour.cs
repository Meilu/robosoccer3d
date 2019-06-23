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
            if (_rulesHandler.checkForRulesToStartGame()) {
                _rulesHandler.checkForRulesDuringTheGame();
            };
        }
    }

    public class SoccerRulesHandler
    {
        public bool checkForRulesToStartGame()
        {
            return true;
        }

        public void checkForRulesDuringTheGame()
        {
            Console.Write("checking rules during the game");
        }
    }
}