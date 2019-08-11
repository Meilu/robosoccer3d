using DataModels;
using UnityEngine;

namespace EventSystem.Events
{
    public class EndMatchEvent : BaseEvent
    {
        public Match match { get; set; }
        public EndMatchEvent(Match match)
        {
            this.match = match;

        }
    }
}