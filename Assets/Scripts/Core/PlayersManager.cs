using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using UnityEngine;

namespace Core
{
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager Instance = null;
//        [SerializeField]
//        private List<Damageable> players;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            
//            players = FindObjectsOfType<Damageable>().ToList();
//
//            GameEvents.OnUISetUpEvent?.Invoke(players);
        }
    }
}