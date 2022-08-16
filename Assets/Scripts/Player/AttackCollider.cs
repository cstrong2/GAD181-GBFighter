using Interfaces;
using UnityEngine;

namespace Player
{
    public class AttackCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            var otherDamageable = otherGameObject.GetComponent<IDamageable>();
            var otherPlayerInstance = otherGameObject.GetComponent<PlayerInstance>();
            var thisPlayerInstance = this.GetComponentInParent<PlayerInstance>();
            if (otherDamageable != null && otherPlayerInstance != thisPlayerInstance)
            {
                otherDamageable.DoDamage(10);
            }
        }
    }
}