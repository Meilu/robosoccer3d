using Sensors;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (RobotVisionSensor))]
public class FovEditor : Editor
{
    
    
    void OnSceneGUI() {
        RobotVisionSensor fow = (RobotVisionSensor)target;
        Handles.color = Color.red;
        var position = fow.transform.position;
        
        Handles.DrawWireArc (position, Vector3.back, Vector2.left, 360, fow.viewRadius);
        
        Vector3 viewAngleA = fow.DirFromAngle (-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle (fow.viewAngle / 2, false);

        Handles.DrawLine (position, position + viewAngleA * fow.viewRadius);
        Handles.DrawLine (position, position + viewAngleB * fow.viewRadius);
    }
}
