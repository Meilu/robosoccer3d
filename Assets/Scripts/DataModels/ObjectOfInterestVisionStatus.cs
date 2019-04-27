using UnityEngine.Events;

namespace DataModels
{
    public class ObjectOfInterestVisionStatus
    {
        public string ObjectName;
        public bool IsInsideVisionAngle = false;
        public bool IsWithinDistance = false;
    }
}