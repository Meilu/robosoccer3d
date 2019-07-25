using UnityEngine;

namespace EventSystem.Events
{
    public class MatchDurationChangedEvent : BaseEvent
    {
        public int MatchDuration { get; set; }
        
        public MatchDurationChangedEvent(int newDuration)
        {
            MatchDuration = newDuration;
        }
    }
}