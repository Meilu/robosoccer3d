using Actuators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RobotBattery : MonoBehaviour
{
    public bool BatteryEmpty = false;
    private float BatteryPercentage = 1.0f;
    private const float drain = 0.3f;
    private const float recharge = 0.1f;
    private RobotMotorAction _RobotMotorAction;
    private RobotActuator _RobotActuator;
    private bool Boost = true;
    private Color _BatteryColor;

    //Made the colors public so we can tweak a bit and see what looks good
    public Color BatteryFullColor;
    public Color BatteryEmptyColor;
    
    // Start is called before the first frame update
    void Awake()
    {
        _RobotActuator = GetComponentInParent<RobotActuator>();
        _BatteryColor = gameObject.GetComponent<Renderer>().material.color = BatteryFullColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (_RobotActuator.activeRobotActionState == null)
            return;
        
        //Checks whether Robot is boosting. For now only boosting drains the battery
        Boost = _RobotActuator.activeRobotActionState.MotorAction == RobotMotorAction.BoostForward;

        //Gradually changes it's battery color. 
        _BatteryColor = gameObject.GetComponent<Renderer>().material.color = Color.Lerp(BatteryEmptyColor, BatteryFullColor, BatteryPercentage);

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
            //Maybe make this into an event so other scripts can access it easier
            BatteryEmpty = true;
        }
        else BatteryEmpty = false;
    }
}
