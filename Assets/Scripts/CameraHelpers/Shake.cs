using System;
using DG.Tweening;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace CameraHelpers
{
    [Serializable]
    struct ShakeAnimation
    {
        public float duration;
        public float strength;
        public int vibrato;
        public int randomness;
    }
    
    public class Shake : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        
        [SerializeField] private ShakeAnimation lightShake;
        [SerializeField] private ShakeAnimation heavyShake;
        
        private void OnEnable()
        {
            GameEvents.OnAttackLandedEvent += DoShakeLight;
            GameEvents.OnJumpLandedEvent += DoShakeHeavy;
        }
        
        private void OnDisable()
        {
            GameEvents.OnAttackLandedEvent -= DoShakeLight;
            GameEvents.OnJumpLandedEvent -= DoShakeHeavy;
        }

        private void DoShakeLight()
        {
            cam.DOShakePosition(lightShake.duration, lightShake.strength, lightShake.vibrato, lightShake.randomness);
        }
        
        private void DoShakeHeavy()
        {
            cam.DOShakePosition(heavyShake.duration, heavyShake.strength, heavyShake.vibrato, heavyShake.randomness);
        }
        
    }
}