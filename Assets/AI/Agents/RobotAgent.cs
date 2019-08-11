using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System.Linq;
using DataModels;
using Actuators;
using Sensors;
using RobotActionStates;

namespace AI {

public class RobotAgent : Agent
{
    Rigidbody rBody;
    RobotActuator actuator;
    public RobotVisionSensorBehaviour visionSensor;
    Vector3 start;
    int step = 0;

    void Start() {
        rBody = GetComponent<Rigidbody>();
        actuator = GetComponent<RobotActuator>();
        start = transform.position;
    }

    public override void AgentReset() {
        transform.position = start;
    }
    
    public override void CollectObservations() {
        AddVectorObs(this.transform.position);
        // Agent velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);

        CollectObjectOfInterestVisionStatuses();
    }

    public override void AgentAction(float[] action, string actionText) {
        RobotMotorAction motorAction = (RobotMotorAction)(int)action[0];
        RobotWheelAction wheelAction = (RobotWheelAction)(int)action[1];
        RobotLegAction legAction = (RobotLegAction)(int)action[2];
        RobotArmAction armAction = (RobotArmAction)(int)action[3];

        actuator.ExecuteRobotAction(new RobotActionState(armAction, legAction, motorAction, wheelAction));

        DetermineReward();
        step++;
    }

    void DetermineReward() {
        var soccerBallObjectOfInterest = visionSensor.objectOfInterestVisionStatuses.FirstOrDefault(x => x.ObjectName == Settings.SoccerBallObjectName);
        float distance = Vector3.Distance(transform.position, soccerBallObjectOfInterest.GameObjectToFind.transform.position);
        if(soccerBallObjectOfInterest.IsInsideVisionAngle) {
            SetReward(1.0f);
        }
        if(step >= 500) {
            Done();
            step = 0;
        }
    }

    void CollectObjectOfInterestVisionStatuses() {
        foreach(ObjectOfInterestVisionStatus status in visionSensor.objectOfInterestVisionStatuses) {
            CollectObjectOfInterestVisionStatus(status);
        }
    }

    void CollectObjectOfInterestVisionStatus(ObjectOfInterestVisionStatus status) {
        AddVectorObs(status.IsInsideVisionAngle ? 1 : 0);
        AddVectorObs(status.IsWithinDistance ? 1 : 0);
        AddVectorObs(status.MinimunDistance);
    }
}


}