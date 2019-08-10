using System;
using DataModels;
using EventSystem;
using EventSystem.Events;
using PhysReps;
using UnityEngine;

namespace Game
{
    public class MatchControllerBehaviour : MonoBehaviour
    {
        public TeamBehaviour homeTeamBehaviour;
        public TeamBehaviour awayTeamBehaviour;

        private MatchController _matchController;
        private void Start()
        {
            _matchController = new MatchController();
            EventManager.Instance.AddListener<StartMatchEvent>(StartMatchEventListener);
        }

        public void StartMatchEventListener(StartMatchEvent e)
        {
            // Initialize the teams which will place all their players on the field.
            homeTeamBehaviour.InitializeTeam(e.match.HomeTeam);
            awayTeamBehaviour.InitializeTeam(e.match.AwayTeam);
            
            _matchController.match = e.match;
            _matchController.SetStartGameTime();
        }
    }

    public class MatchController
    {
        public Match match;
        
        public void SetStartGameTime()
        {
            Console.WriteLine("setting starttime");
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