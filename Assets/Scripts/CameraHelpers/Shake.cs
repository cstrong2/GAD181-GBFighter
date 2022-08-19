using System;
using DG.Tweening;
using Events;
using UnityEngine;

namespace CameraHelpers
{
    public class Shake : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        private void OnEnable()
        {
            GameEvents.OnAttackLandedEvent += DoShake;
        }
        
        private void OnDisable()
        {
            GameEvents.OnAttackLandedEvent -= DoShake;
        }

        private void DoShake()
        {
            cam.DOShakePosition(.02f, .2f, 1, 45);
        }
    }
}