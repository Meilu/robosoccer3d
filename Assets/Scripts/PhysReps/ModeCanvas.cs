using UnityEngine;

namespace PhysReps
{
    public class ModeCanvas : MonoBehaviour
    {
        private GameObject _robotCharacterContainer;
        // Start is called before the first frame update
        void Start()
        {
            _robotCharacterContainer = transform.parent.Find("robot").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            // Always keep the canvas position in sync with the robot character.
            transform.position = _robotCharacterContainer.transform.position;

        }
    }
}
