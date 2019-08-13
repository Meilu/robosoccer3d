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
        private void Awake()
        {
            _matchController = new MatchController();
        }

        public void StartMatch(Match match)
        {
            // Initialize the teams which will place all their players on the field.
            homeTeamBehaviour.InitializeTeam(match.HomeTeam);
            awayTeamBehaviour.InitializeTeam(match.AwayTeam);
            
            _matchController.match = match;
            _matchController.SetStartGameTime();
            
            // All good, throw the startmatchevent
            EventManager.Instance.Raise(new StartMatchEvent(match));
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