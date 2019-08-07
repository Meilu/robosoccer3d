using DataModels;
using UnityEngine;

namespace EventSystem.Events
{
    public class StartMatchEvent : BaseEvent
    {
        public int MatchDuration { get; set; }
        
        public StartMatchEvent(Match match)
        {
        }
    }
}