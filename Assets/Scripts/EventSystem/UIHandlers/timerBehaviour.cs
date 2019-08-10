using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using EventSystem.Events;

public class timerBehaviour : MonoBehaviour
{
    private Text _textComponent;
    private TimerText _timer;

    private bool GameHasEnded = false;
    public float matchDuration;

    // Start is called before the first frame update
    void Start()
    {
        _timer = new TimerText(matchDuration);
        _textComponent = GetComponent<Text>();
        _textComponent.text = "START";

        EventManager.Instance.AddListener<StartMatchEvent>(StartMatchListener);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void StartMatchListener(StartMatchEvent startMatchEvent)
    {
        Console.WriteLine("starting match");
        InvokeRepeating("UpdateTimerText", 1.0f, 1.0f);
    }
    void UpdateTimerText()
    {
        //sets the text for the timer
        _textComponent.text = _timer.DetermineMatchTimerText();

        if (_timer.IsMatchEnded())
        {
            CancelInvoke("UpdateTimerText");

            EventManager.Instance.Raise(
                new EndMatchEvent(
                    new DataModels.Match(null, null)
                ));
        }
    }
}

public class TimerText {
    private float duration = 0;
    private float maxDuration;

    public TimerText(float maxDuration)
    {
        this.maxDuration = maxDuration;
    }
    public bool IsMatchEnded()
    {
        return duration >= maxDuration;
    }
    public string DetermineMatchTimerText()
    {
        duration++;

        float minutes = Mathf.Floor(duration / 60);
        float seconds = Mathf.RoundToInt(duration%60);

        string minutesDisplay = "0";
        string secondsDisplay = "0";
        if(minutes < 100) {
            minutesDisplay = ""+ minutes;
        }
        if(seconds < 100) {
            secondsDisplay = "" + Mathf.RoundToInt(seconds).ToString();
        }

        return minutesDisplay + ":" + secondsDisplay;
    }
}

