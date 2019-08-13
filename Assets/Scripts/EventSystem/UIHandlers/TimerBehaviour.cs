
using System;
using UnityEngine;
using UnityEngine.UI;

using EventSystem.Events;

namespace EventSystem.UIHandlers
{

    public class TimerBehaviour : MonoBehaviour
    {
        private Text _textComponent;
        private TimerText _timer;

        public float matchDuration;

        void Start()
        {
            _timer = new TimerText(matchDuration);
            _textComponent = GetComponent<Text>();
            
            EventManager.Instance.AddListener<EndMatchEvent>(EndMatchEventListener);

        }

        void EndMatchEventListener(EndMatchEvent e)
        {
            print("match ended");
            StopMatchTimer();
        }
        public void StartMatchTimer()
        {
           // InvokeRepeating("UpdateTimerText", 1.0f, 1.0f);
            EventManager.Instance.Raise(new MatchTimerStartedEvent());
        }

        public void StopMatchTimer()
        {
            _timer.ResetDuration();
            CancelInvoke("UpdateTimerText");
            EventManager.Instance.Raise(new MatchTimerEndedEvent());
        }

        void UpdateTimerText()
        {
            //sets the text for the timer
            _textComponent.text = _timer.DetermineMatchTimerText();
            
            if (_timer.IsMatchEnded())
            {
                StopMatchTimer();
            }
        }
    }
    
    public class TimerText
    {
        private float maxDuration;
        private float duration = 0;

        public TimerText(float maxDuration)
        {
            this.maxDuration = maxDuration;
        }

        public void ResetDuration()
        {
            duration = 0;
        }

        public bool IsMatchEnded()
        {
            return duration >= maxDuration;
        }
        public string DetermineMatchTimerText()
        {
            duration++;

            float minutes = Mathf.Floor(duration / 60);
            float seconds = Mathf.RoundToInt(duration % 60);

            string minutesDisplay = "0";
            string secondsDisplay = "0";
            if (minutes < 100)
            {
                minutesDisplay = "" + minutes;
            }
            if (seconds < 100)
            {
                secondsDisplay = "" + Mathf.RoundToInt(seconds).ToString();
            }

            return minutesDisplay + ":" + secondsDisplay;
        }   
    }
}

