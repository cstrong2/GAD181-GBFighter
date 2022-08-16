using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using UnityEngine;

namespace ScriptableObjects
{
    public class FightState : ScriptableObject
    {
        [SerializeField]
        private List<bool> _playersDead = new();

        public List<bool> PlayersDead
        {
            get => _playersDead;
            set
            {
                _playersDead = value;
                var isDead = PlayersDead.FindAll(b => b);
                var check = PlayersDead.Except(isDead).ToList();
                if (check.Count <= 1)
                {
                    GameEvents.OnPlayerWonEvent?.Invoke(PlayersDead.FindIndex(b => b == false));
                }
            }
        }

        private void OnEnable()
        {
            GameEvents.OnPlayerDiedEvent += SetPlayerDead;
        }
        
        private void OnDisable()
        {
            GameEvents.OnPlayerDiedEvent -= SetPlayerDead;
        }
        
        private void SetPlayerDead(int playerid)
        {
            PlayersDead[playerid] = true;
        }
    }
}