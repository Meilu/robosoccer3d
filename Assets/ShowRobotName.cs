using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModels;
using UnityEngine.UI;

public class ShowRobotName : MonoBehaviour
{
    private RectTransform _RobotNameTransform;
    private Robot _robot;
    private Text RobotTextName;

    // Start is called before the first frame update
    void Start()
    {
        _RobotNameTransform = GetComponent<RectTransform>();

        //Seems this isn't really working? How else can I get it's name?
        _robot = GetComponentInParent<Robot>();

        RobotTextName = GetComponent<Text>();
        RobotTextName.text = _robot.Name;
    }

    // Update is called once per frame
    void Update()
    {
        _RobotNameTransform.eulerAngles = new Vector3(_RobotNameTransform.eulerAngles.x, 0f, _RobotNameTransform.eulerAngles.z);
    }
}
