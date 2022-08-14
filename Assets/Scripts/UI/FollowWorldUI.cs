using UnityEngine;

namespace UI
{
    public class FollowWorldUI : MonoBehaviour
    {
        public Transform lookAtPosition;
        [SerializeField] private Vector3 offset;
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            Vector3 pos = cam.WorldToScreenPoint(lookAtPosition.position + offset);

            if (transform.position != pos)
                transform.position = pos;
        }
    }
    
    
}