using EventSystem;
using EventSystem.Events;
using EventSystem.UIHandlers;
using UnityEngine;

namespace MiscObjects
{
    public class StartMatchButton : MonoBehaviour
    {
        private TimerBehaviour _timer;
        // Start is called before the first frame update
        void Start()
        {
            _timer = GameObject.Find("MatchTimer").GetComponent<TimerBehaviour>();
        
            print("Adding button listener");
            
            EventManager.Instance.AddListener<MatchTimerStartedEvent>(MatchTimerStartedListener);
            EventManager.Instance.AddListener<MatchTimerEndedEvent>(MatchTimerEndedListener);
        }
        
        public void OnStartMatchButtonClick()
        {
            _timer.StartMatchTimer();
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
