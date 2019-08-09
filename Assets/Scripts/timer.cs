using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Eventsystem.UIHandlers
{
    public class TimerBehaviour : MonoBehaviour
    {
        private Text _textComponent;
        private Timer _timer;

        public float matchDuration;
        // Start is called before the first frame update
        void Start()
        {
            _textComponent = GetComponent<Text>();
            _timer.countdownFromMatchDuration(_textComponent);
        }

        // Update is called once per frame
        void Update()
        {
            _timer.showMatchTimer(_textComponent.text);
        }
    }

    public class Timer
    {

        void countdownFromMatchDuration(Text text)
        {
            InvokeRepeating("decreaseMatchDuration(_textComponent)", 1.0f, 1.0f);
        }

        void showMatchTimer(Text text)
        {
            if (matchDuration == 0)
            {
                return "finished";
            }
        }
    
        void decreaseMatchDuration(Text text)
        {
            if (matchDuration > 0)
            {
                matchDuration--;
                updateTimerLabel();
            }
            else
            {
                CancelInvoke("decreaseMatchDuration");
                //EventManager.Instance.TriggerEvent(EventType.MatchFinished);
                print("match finished");
            }
        }

        void updateTimerLabel()
        {
            float minutes = Mathf.Floor(matchDuration / 60);
            float seconds = Mathf.RoundToInt(matchDuration%60);

            string minutesDisplay = "0";
            string secondsDisplay = "0";
            if(minutes < 100) {
                minutesDisplay = ""+ minutes;
            }
            if(seconds < 100) {
                secondsDisplay = "" + Mathf.RoundToInt(seconds).ToString();
            }

            _textComponent.text = minutesDisplay + ":" + secondsDisplay;
        }
    }
}

