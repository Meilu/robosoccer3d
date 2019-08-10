using DataModels;
using UnityEngine;

namespace EventSystem.Events
{
    public class StartMatchEvent : BaseEvent
    {
        public int MatchDuration { get; set; }
        public Match match { get; set; }
        public StartMatchEvent(Match match)
        {
            this.match = match;
        }
    }
}