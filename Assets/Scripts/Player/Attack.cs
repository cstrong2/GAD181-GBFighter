using Interfaces;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Transform toAttachTo;
        private BoxCollider collider = new();
        [SerializeField] private Vector3 colliderSize = new Vector3(0.3f, 0.3f, 0.3f);

        private void OnEnable()
        {
            toAttachTo = GetComponentsInChildren<Transform>().ToList().Find(n => n.gameObject.name.Contains("LeftHand"));
            collider = toAttachTo.gameObject.AddComponent<BoxCollider>();
            collider.size = colliderSize;
            collider.isTrigger = true;
            collider.enabled = false;

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
            collider.enabled = true;
        }
        
        public void DeactivateAttackTrigger()
        {
            collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherDamage = other.gameObject.GetComponent<IDamageable>();
            if (otherDamage != null)
            {
                otherDamage.DoDamage(-10);
            }
        }

    }
}