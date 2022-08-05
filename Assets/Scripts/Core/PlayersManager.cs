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
        private List<PlayerController> players;

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
            
            players = FindObjectsOfType<PlayerController>().ToList();
            GameEvents.OnUISetUpEvent?.Invoke(players);
        }
    }
}