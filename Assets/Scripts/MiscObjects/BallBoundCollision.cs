using UnityEngine;

namespace MiscObjects
{
    public class BallBoundCollision : MonoBehaviour
    {
        public BallBoundType BallBoundType;
        public void OnTriggerExit(Collider other)
        {
            // Unity does not support a way to send both collisions on the ontriggerexit function to the parent
            // So we are doing it ourselfs.
            gameObject.GetComponentInParent<BallBoundsCollision>().HandleCollision(gameObject, other);;
        }
    }
}
