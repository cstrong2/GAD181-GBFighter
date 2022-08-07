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
            inputManager.onPlayerJoined += AddPlayer;

        }

        private void OnDisable()
        {
            GameEvents.OnLoadGameDataEvent -= SetGameData;
            inputManager.onPlayerJoined -= AddPlayer;
        }

        private void AddPlayer(PlayerInput player)
        { 
            if (players.Count == _maxPlayers)
                return;
//            // We need a message to show if max players is reached or something?
//            
            
            player.transform.parent = this.transform;
            // This sets up the player data for a new player
            var playerInstance = player.GetComponent<PlayerInstance>();
            playerInstance.playerInput = player;
            playerInstance.playerInstanceData = ScriptableObject.CreateInstance<PlayerData>();
            playerInstance.playerInstanceData.PlayerID = player.playerIndex;
            playerInstance.name = playerInstance.playerInstanceData.PlayerLabel;
            playerInstance.playerInstanceData.name = playerInstance.playerInstanceData.PlayerLabel;
            players.Add(playerInstance);
            Debug.Log(players.Count +" players count");
            
            GameEvents.OnNewPlayerJoinedEvent?.Invoke(player.playerIndex);
        }

        void SetGameData(int maxPlayers)
        {
            if(startUpRan == false)
            {
                // We need to add one player because there will always be at least one.
                players.Capacity = maxPlayers;
                _maxPlayers = maxPlayers;
                startUpRan = true;
            }
        }
    }
}