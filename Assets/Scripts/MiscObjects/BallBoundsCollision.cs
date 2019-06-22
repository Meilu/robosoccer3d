using System;
using UnityEngine;

namespace MiscObjects
{
    public class BallBoundsCollision : MonoBehaviour
    {
        public void HandleCollision(GameObject fromObject, Collider other)
        {
            var ballBoundCollision = fromObject.GetComponent<BallBoundCollision>();

            if (ballBoundCollision == null)
                return;

            var type = ballBoundCollision.BallBoundType;
            
            // Do something with type here.
        }
    }
}
