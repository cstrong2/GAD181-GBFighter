using System;
using Interfaces;
using System.Linq;
using Audio;
using Events;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] public AudioClipList hitSounds;
        [SerializeField] public AudioClipList attackSounds;
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
            toAttachTo = GetComponentsInChildren<Transform>().ToList().Find(n => n.gameObject.name.Contains("Left Hand") || n.gameObject.name.Contains("Left_Hand") || n.gameObject.name.Contains("LeftHand"));
            if(toAttachTo)
            {
                _col = toAttachTo.gameObject.AddComponent<BoxCollider>();

                

                _col.size = colliderSize;
                _col.isTrigger = true;
                _col.enabled = false;
            }
        }

        private void Start()
        {
            if (hitSounds != null)
            {
                var attackCollider = _col.AddComponent<AttackCollider>();

                attackCollider.collisionClips = hitSounds;
            }
        }

        private void OnDrawGizmos()
        {
            if (_col == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_col.transform.position, _col.size);
        }

        public void ActivateAttackTrigger()
        {
            if (_col == null)
                return;
            _col.enabled = true;
            GameEvents.OnAudioCollisionEvent?.Invoke(attackSounds.GetRandomClip());
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