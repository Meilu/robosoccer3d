using System;
using EventSystem;
using EventSystem.Events;
using EventSystem.UIHandlers;
using UnityEngine;

namespace MiscObjects
{
    public class StartMatchButton : MonoBehaviour
    {
        private TimerBehaviour _timer;

        private void Awake()
        {
            //TODO:  The order in which the listeners are bound in the event manager actually matters :(
            // Need to find a solution for that, for now i need to make sure the button adds the listeners first
            // So i add them in this awake method
            EventManager.Instance.AddListener<MatchTimerStartedEvent>(MatchTimerStartedListener);
            EventManager.Instance.AddListener<MatchTimerEndedEvent>(MatchTimerEndedListener);
            gameObject.SetActive(false);
        }

        public void SetActive()
        {
            gameObject.SetActive(true);
        }

        void MatchTimerEndedListener(MatchTimerEndedEvent e)
        {
            gameObject.SetActive(true);
        }
        void MatchTimerStartedListener(MatchTimerStartedEvent e)
        {
            gameObject.SetActive(false);
        }
    }
}
