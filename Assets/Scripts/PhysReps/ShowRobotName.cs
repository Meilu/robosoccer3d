using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModels;
using Planners;

public class ShowRobotName : MonoBehaviour
{
    private RectTransform _RobotNameTransform;
    private RobotPlanner _robotPlanner;
//  private TextMeshProUGUI RobotTextName;

    // Start is called before the first frame update
    void Start()
    {
        _RobotNameTransform = GetComponent<RectTransform>();

        //Seems this isn't really working? How else can I get it's name?
        _robotPlanner = GetComponentInParent<RobotPlanner>();

//       RobotTextName = GetComponent<TextMeshProUGUI>();
//       RobotTextName.SetText(_robotPlanner.RobotModel.Name);
    }

    // Update is called once per frame
    void Update()
    {
        _RobotNameTransform.eulerAngles = new Vector3(_RobotNameTransform.eulerAngles.x, 0f, _RobotNameTransform.eulerAngles.z);
    }
}
