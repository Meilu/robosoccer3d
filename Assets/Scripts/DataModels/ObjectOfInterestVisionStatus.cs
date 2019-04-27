using UnityEngine;
using UnityEngine.Events;

namespace DataModels
{
    public class ObjectOfInterestVisionStatus
    {
        public string ObjectName;
        public bool IsInsideVisionAngle = false;
        public bool IsWithinDistance = false;
        // The game object will be stored here so we dont have to do gameobject.Find all the time (because it is expensive).
        public GameObject GameObjectToFind;
    }
}