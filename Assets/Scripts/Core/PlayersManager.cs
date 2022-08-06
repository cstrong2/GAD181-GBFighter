using System.Collections.Generic;
using Events;
using Player;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager Instance = null;

        private PlayerInputManager inputManager;
        
        [SerializeField]
        private List<PlayerInstance> players;

        private int _maxPlayers;

        private bool startUpRan = false;
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
            
            if(inputManager == null)
                inputManager = GetComponent<PlayerInputManager>();
        }

        private void OnEnable()
        {
            GameEvents.OnLoadGameDataEvent += SetGameData;
            GameEvents.OnAddNewPlayerEvent += AddPlayer;
        }

        private void OnDisable()
        {
            GameEvents.OnLoadGameDataEvent -= SetGameData;
            GameEvents.OnAddNewPlayerEvent -= AddPlayer;
        }

        void AddPlayer()
        {
//            if (players.Count == _maxPlayers)
//                return;
//            // We need a message to show if max players is reached or something?
//            

            // This sets up the player data for a new player
            GameObject newPlayer = Instantiate(new GameObject(), transform);
            var newPlayerBrain = newPlayer.AddComponent<PlayerInstance>();
            newPlayerBrain.playerInstanceData = ScriptableObject.CreateInstance<PlayerData>();
            players.Add(newPlayerBrain);
            newPlayerBrain.playerInstanceData.PlayerID = players.FindIndex(m => m == newPlayerBrain);
            newPlayerBrain.playerInstanceData.name = newPlayerBrain.playerInstanceData.PlayerLabel;
            newPlayerBrain.name = newPlayerBrain.playerInstanceData.PlayerLabel;
            
            Debug.Log(players.Count +" players count");
            
        }

        void SetGameData(int maxPlayers)
        {
            if(startUpRan == false)
            {
                // We need to add one player because there will always be at least one.
                AddPlayer();
                players.Capacity = maxPlayers;
                _maxPlayers = maxPlayers;
                startUpRan = true;
            }
        }
    }
}