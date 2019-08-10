using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using EventSystem.Events;
using System;
using UnityEngine.UI;

public class StartMatchButton : MonoBehaviour
{
    GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("StartButton");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnStartMatchButtonClick()
    {
        Console.WriteLine("start knop");
        // Finally, raise an event that the match has started along with the match itself.
        EventManager.Instance.Raise(
            new StartMatchEvent(
                new DataModels.Match(null, null)
            ));
        button.SetActive(false);

    }
}
