using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using EventSystem.Events;
using System;
using UnityEngine.UI;
using EventSystem.UIHandlers;

public class StartMatchButton : MonoBehaviour
{
    private TimerBehaviour _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = GameObject.Find("MatchTimer").GetComponent<TimerBehaviour>();
        
        EventManager.Instance.AddListener<EndMatchEvent>(EndMatchListener);
    }

    public void OnStartMatchButtonClick()
    {
        _timer.StartMatchTimer();
        gameObject.SetActive(false);

    }

    void EndMatchListener(EndMatchEvent endMatchEvent)
    {
        Console.WriteLine("ending match");
        gameObject.SetActive(true);
    }
}
