using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<EventType, UnityEvent> _eventDictionary;

    private static EventManager _eventManager;

    public static EventManager Instance
    {
        get
        {
            if (_eventManager)
                return _eventManager;
            
            _eventManager = FindObjectOfType<EventManager>();

            if (!_eventManager)
                return null;
            
            _eventManager.Init();
            return _eventManager;
        }
    }

    /// <summary>
    /// Will initialise everything needed to have a working event manager.
    /// </summary>
    private void Init()
    {
        if (_eventDictionary != null)
            return;
        
        _eventDictionary = new Dictionary<EventType, UnityEvent>();
    }

    /// <summary>
    /// Binds a unityaction to an eventName, which will be triggered when the event is invoked.
    /// </summary>
    /// <param name="eventType">The type of the event</param>
    /// <param name="listener">The listener that will be triggered when the event is invoked.</param>
    public void StartListeningToEvent(EventType eventType, UnityAction listener)
    {

        if (!_eventDictionary.TryGetValue(eventType, out var thisEvent))
        {
            thisEvent = new UnityEvent();
            _eventDictionary.Add(eventType, thisEvent);
        }
        
        // Adds the UnityAction as a listener to this event.
        // We can bind as many unityactions to an event as we want.:O
        thisEvent.AddListener(listener);
    }

    /// <summary>
    /// Call this function to remove a listener from an event.
    /// This is needed for when we want to cleanup objects and avoid memory leaks
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public void StopListeningToEvent(EventType eventType, UnityAction listener)
    {
        if (_eventDictionary.TryGetValue(eventType, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Trigger an event by eventType.
    /// </summary>
    /// <param name="eventType">The type of the event.</param>
    public void TriggerEvent(EventType eventType)
    {
        if (_eventDictionary.TryGetValue(eventType, out var thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
