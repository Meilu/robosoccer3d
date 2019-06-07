using JetBrains.Annotations;
using UnityEngine;

namespace DataModels
{
    public abstract class ObjectOfInterestStatus<T>
    {
        public abstract T Copy();
        public GameObject GameObjectToFind;
        public string ObjectName;
        
        [CanBeNull]
        public string TagName;
    }
}