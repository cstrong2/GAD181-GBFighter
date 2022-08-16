using Interfaces;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Transform toAttachTo;
        private BoxCollider _col = new();
        [SerializeField] private Vector3 colliderSize = new (0.3f, 0.3f, 0.3f);

        public Transform ToAttachTo
        {
            get => toAttachTo;
            set => toAttachTo = value;
        }
        
        private void OnEnable()
        {
            toAttachTo = GetComponentsInChildren<Transform>().ToList().Find(n => n.gameObject.name.Contains("LeftHand"));
            _col = toAttachTo.gameObject.AddComponent<BoxCollider>();
            _col.AddComponent<AttackCollider>();
            _col.size = colliderSize;
            _col.isTrigger = true;
            _col.enabled = false;
        }

        private void OnDrawGizmos()
        {
            if (_col == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_col.transform.position, _col.size);
        }

        public void ActivateAttackTrigger()
        {
            if (_col == null)
                return;
            _col.enabled = true;
            Debug.Log("We done a punch");
        }
        
        public void DeactivateAttackTrigger()
        {
            if (_col == null)
                return;
            _col.enabled = false;
        }
        
    }
}