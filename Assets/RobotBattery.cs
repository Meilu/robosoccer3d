using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RobotBattery : MonoBehaviour
{
    public bool BatteryEmpty = false;
    private float BatteryPercentage = 1.0f;
    private const float drain = 0.2f;
    private const float recharge = 0.1f;
    private Image _BatteryImage;
    private RobotMotorAction _RobotMotorAction;
    private bool Boost = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _BatteryImage = GetComponent<Image>();
        _RobotMotorAction = GetComponentInParent<RobotMotorAction>();
    }

    // Update is called once per frame
    void Update()
    {
        //This shit is supposed to check whether the robot is boosting or not....
        Boost = (_RobotMotorAction == RobotMotorAction.BoostForward) ? true : false;

        if (Boost && BatteryPercentage > 0.01)
        {
            BatteryPercentage -= drain * Time.deltaTime;
        }
        else if (BatteryPercentage < 1)
        {
            BatteryPercentage += recharge * Time.deltaTime;
        }

        if (BatteryPercentage < 0.01)
        {
            BatteryEmpty = true;
        }
        else BatteryEmpty = false;

       _BatteryImage.fillAmount = BatteryPercentage;
    }
}
