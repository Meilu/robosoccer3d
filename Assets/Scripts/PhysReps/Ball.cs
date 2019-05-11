using System;
using UnityEngine;
using UnityEngine.Events;

namespace PhysReps
{
    public class Ball : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.name == "frontLegs")
            {
                // The ball is hit by the frontlegs of a robot, add some force.
                Vector3 direction = (other.transform.position - transform.position).normalized;
                GetComponent<Rigidbody>().AddForce(-direction * 0.8f, ForceMode.Impulse);
            }
                
        }
    }
}