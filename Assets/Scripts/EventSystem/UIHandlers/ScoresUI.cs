using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem.Events;
using EventSystem;

public class ScoresUI : MonoBehaviour
{
    public GameObject scores;
    private Text HomeTextComponent;
    private Text AwayTextComponent;
    public int HomeScore = 0;
    public int AwayScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        HomeTextComponent = scores.transform.GetChild(0).gameObject.GetComponent<Text>();
        AwayTextComponent = scores.transform.GetChild(1).gameObject.GetComponent<Text>();
        EventManager.Instance.AddListener<BallCrossedGoalLineEvent>(BallCrossedGoalLineEventListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BallCrossedGoalLineEventListener(BallCrossedGoalLineEvent e)
    {
        if(e.BallBoundType == BallBoundType.LeftGoalBound)
        {
            HomeTextComponent.text = HomeScore++.ToString();
        }
        else if (e.BallBoundType == BallBoundType.RightGoalBound)
        {
            AwayTextComponent.text = AwayScore++.ToString();
        }
    }
}
