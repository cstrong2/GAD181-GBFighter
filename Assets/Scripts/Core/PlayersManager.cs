using System.Collections.Generic;
using System.Linq;
using Events;
using UnityEngine;

namespace Core
{
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager Instance = null;
        
        [SerializeField]
        private List<PlayerBrain> players;

        [SerializeField] private int maxPlayers = 4;
        public int MaxPlayers => maxPlayers;

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
            
//            players = FindObjectsOfType<PlayerBrain>().ToList();
//            GameEvents.OnUISetUpEvent?.Invoke(players);
        }

        private void OnEnable()
        {
            GameEvents.OnAddNewPlayerEvent += AddPlayer;
        }

        private void OnDisable()
        {
            GameEvents.OnAddNewPlayerEvent -= AddPlayer;
        }

        void AddPlayer()
        {
            Instantiate(new GameObject().AddComponent<Player.PlayerBrain>(), transform);
        }
    }
}