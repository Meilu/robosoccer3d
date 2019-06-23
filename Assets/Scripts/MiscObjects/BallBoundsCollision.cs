using System;
using EventSystem;
using EventSystem.Events;
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

            if (other == null || other.gameObject.name != Settings.SoccerBallObjectName)
                return;
            
            var type = ballBoundCollision.BallBoundType;

            switch (type)
            {
                case BallBoundType.LeftGoalBound:
                case BallBoundType.RightGoalBound:
                    EventManager.Instance.Raise(
                        new BallCrossedGoalLineEvent(
                                other.gameObject.transform.position,
                                type
                            ));
                    break;
                default:
                    return;
            }
        }
    }
}
