using System.Linq;
using UnityEngine;

namespace Player
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Transform toAttachTo;
        private BoxCollider collider;
        [SerializeField] private Vector3 colliderSize = new Vector3(0.3f, 0.3f, 0.3f);

        private void OnEnable()
        {
            toAttachTo = GetComponentsInChildren<Transform>().ToList().Find(n => n.gameObject.name.Contains("LeftHand"));
            collider = toAttachTo.gameObject.AddComponent<BoxCollider>();
            collider.size = colliderSize;
            collider.isTrigger = true;
        }

        private void OnDrawGizmos()
        {
            if (collider == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(collider.transform.position, collider.size);
        }

        

        public void ActivateAttackTrigger()
        {
            
        }
        
        public void DeactivateAttackTrigger()
        {
            
        }
        
    }
}