using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private Text _textComponent;

    public float matchDuration;
    // Start is called before the first frame update
    void Start()
    {
        _textComponent = GetComponent<Text>();
        InvokeRepeating("decreaseMatchDuration", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (matchDuration == 0)
        {
            _textComponent.text = "finished";
        }
    }

    void decreaseMatchDuration()
    {
        if (matchDuration > 0)
        {
            matchDuration--;
            updateTimerLabel();
        }
        else
        {
            CancelInvoke("decreaseMatchDuration");
            EventManager.Instance.TriggerEvent(EventType.MatchFinished);
            print("match finished");
        }
    }

    void updateTimerLabel()
    {
        float minutes = Mathf.Floor(matchDuration / 60);
        float seconds = Mathf.RoundToInt(matchDuration%60);

        string minutesDisplay = "0";
        string secondsDisplay = "0";
        if(minutes < 10) {
            minutesDisplay = "0" + minutes;
        }
        if(seconds < 10) {
            secondsDisplay = "0" + Mathf.RoundToInt(seconds).ToString();
        }

        _textComponent.text = minutesDisplay + ":" + secondsDisplay;
    }
}
