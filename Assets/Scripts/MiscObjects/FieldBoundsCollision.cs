using System;
using EventSystem;
using EventSystem.Events;
using UnityEngine;

namespace MiscObjects
{
    public class FieldBoundsCollision : MonoBehaviour
    {
        public void HandleCollision(GameObject fromObject, Collider other)
        {
            var ballBoundCollision = fromObject.GetComponent<FieldBoundCollision>();

            if (ballBoundCollision == null)
                return;

            if (other.gameObject.name == Settings.SoccerBallObjectName)
            {
                HandleSoccerBallBoundsCollision(ballBoundCollision.fieldBoundType, other.gameObject);
            }
        }

        private void HandleSoccerBallBoundsCollision(FieldBoundType fieldBoundType, GameObject soccerball)
        {
            switch (fieldBoundType)
            {
                case FieldBoundType.SideLineBound:
                case FieldBoundType.LeftGoalBound:
                case FieldBoundType.RightGoalBound:
                    EventManager.Instance.Raise(
                        new BallCrossedLineEvent(
                            soccerball.transform.position,
                            fieldBoundType
                        ));
                    break;
                default:
                    return;
            }
        }
    }
}
