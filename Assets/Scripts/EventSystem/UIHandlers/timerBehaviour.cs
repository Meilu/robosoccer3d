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

    // Start is called before the first frame update
    void Start()
    {
        _textComponent = GetComponent<Text>();
        _textComponent.text = "START";
        InvokeRepeating("UpdateTimerText", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHasEnded)
        {
            CancelInvoke("UpdateTimerText");
        }
        
    }

    void UpdateTimerText()
    {
        //sets the text for the timer
        _textComponent.text = _timer.DetermineMatchTimerText();
    }
}

public class TimerText {

    private float duration = 30f;

    public string DetermineMatchTimerText()
    {
        string MatchPartName = DetermineMatchPartName();
        string CountDownTimer = UpdateCountdownTime();

        return MatchPartName + "/n test /n" + CountDownTimer;
    }

    public string DetermineMatchPartName()
    {
        string name = "MATCH IN PLAY";
        return name;
    }

    public string UpdateCountdownTime()
    {
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

