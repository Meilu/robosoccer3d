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
        EventManager.Instance.AddListener<BallCrossedLineEvent>(BallCrossedLineEventListener);
    }

    void BallCrossedLineEventListener(BallCrossedLineEvent e)
    {
        if(e.FieldBoundType == FieldBoundType.LeftGoalBound)
        {
            HomeTextComponent.text = HomeScore++.ToString();
        }
        else if (e.FieldBoundType == FieldBoundType.RightGoalBound)
        {
            AwayTextComponent.text = AwayScore++.ToString();
        }
    }
}
