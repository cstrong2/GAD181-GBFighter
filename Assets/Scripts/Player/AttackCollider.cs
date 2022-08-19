using Events;
using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class AttackCollider : MonoBehaviour
    {
        public AudioClipList collisionClips;
        private void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            var otherDamageable = otherGameObject.GetComponent<IDamageable>();
            var otherPlayerInstance = otherGameObject.GetComponent<PlayerInstance>();
            var thisPlayerInstance = this.GetComponentInParent<PlayerInstance>();
            if (otherDamageable != null && otherPlayerInstance != thisPlayerInstance)
            {
                otherDamageable.DoDamage(10);
                if (collisionClips)
                    GameEvents.OnAudioCollisionEvent?.Invoke(collisionClips.GetRandomClip());

                GameEvents.OnAttackLandedEvent?.Invoke();
            }

        }
    }
}