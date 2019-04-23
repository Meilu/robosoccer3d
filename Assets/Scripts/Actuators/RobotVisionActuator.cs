using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotVisionActuator : MonoBehaviour
{
    
    private RobotMotorAction _activeRobotMotorAction;
    private RobotWheelAction _activeRobotWheelAction;
    private RobotLegAction _activeRobotLegAction;

    float timeBetweenTriggers = 0.01f;
    private bool actiontriggered = false;

    private float? rotationBefore45DegreesTurn;
    private bool hasExecutedMove;

    public void TriggerAction(RobotMotorAction robotAction)
    {
        if (!actiontriggered && _activeRobotMotorAction != robotAction)
        {
            _activeRobotMotorAction = robotAction;
            actiontriggered = true;
            timeBetweenTriggers = 0.01f;
        }
    }

    public void TriggerAction(RobotWheelAction robotAction)
    {
        if (!actiontriggered && _activeRobotWheelAction != robotAction)
        {
            _activeRobotWheelAction = robotAction;
            actiontriggered = true;
            timeBetweenTriggers = 0.01f;
        }
    }

    public void TriggerAction(RobotLegAction robotAction)
    {
        if (!actiontriggered && _activeRobotLegAction != robotAction)
        {
            _activeRobotLegAction = robotAction;
            actiontriggered = true;
            //We can make this action switch slower?
            timeBetweenTriggers = 0.1f;
        }
    }

    void FixedUpdate()
    {

        if (actiontriggered) { timeBetweenTriggers -= Time.deltaTime; }
        if (timeBetweenTriggers <= 0) { actiontriggered = false; }

        switch (_activeRobotMotorAction)
        {
            case RobotMotorAction.MoveForward:
                // print("Moving forward");
                MoveForward();
                break;
            case RobotMotorAction.MoveBackward:
                // print("Moving backward");
                MoveBackward();
                break;
            case RobotMotorAction.DoNothing:
                //stop moving
                DoNothing();
                break;
            default:
                //stop moving
                DoNothing();
                break;
        }

        switch (_activeRobotWheelAction)
        {
            case RobotWheelAction.TurnRight:
                // print("Turning right");
                TurnRight();
                break;
            case RobotWheelAction.TurnLeft:
                // print("Turning left");
                TurnLeft();
                break;
            case RobotWheelAction.DoNothing:
                //stop moving
                DoNothing();
                break;
            default:
                //stop moving
                DoNothing();
                break;
        }

        switch (_activeRobotLegAction)
        {     
            case RobotLegAction.KickForward:
                print("Shooting");
                KickForward();
                break;
            case RobotLegAction.DoNothing:
                //stop moving
                DoNothing();
                break;
            default:
                //stop moving
                DoNothing();
                break;
        }
    }

    //Here are the motor functions
    public void MoveForward()
    {
        GetComponent<Rigidbody>().velocity = transform.up * 100.0f * Time.deltaTime; 
    }

    public void MoveBackward()
    {
        transform.Translate(Vector2.down * 1.0f * Time.deltaTime);
    }

    //Here are the wheel functions
    public void TurnRight()
    {
        transform.Rotate(Vector3.back * (150.0f * Time.deltaTime));
    }

    public void TurnLeft()
    {
        transform.Rotate(Vector3.back * (-150.0f * Time.deltaTime));
    }

    //Here are the leg(?) functions
    public void KickForward()
    {
        // Test to see if ball keeps moving (will look like a shot)
        GetComponent<Rigidbody>().velocity = Vector2.zero;
        // GetComponent<Rigidbody2D>().velocity = transform.up * 300.0f * Time.deltaTime;
    }

    //TO DO: Here are the arm movements

    //Just like me in the morning
    public void DoNothing()
    {
      //  transform.Translate(Vector2.up * 0.1f * Time.deltaTime);
    }      
}
