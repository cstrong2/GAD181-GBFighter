using System.Collections.Generic;
using Events;
using Player;
using ScriptableObjects;
using UI;
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
        [SerializeField] private GameObject fightSceneCharacterPrefab;

        private const string PlayerMap = "Player", UIMap = "UI";
        
        public List<PlayerInstance> Players
        {
            get => players;
            private set => players = value;
        }
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
            GameEvents.OnFightSceneLoadingEvent += DoFightSetup;
            GameEvents.OnFightSceneHasLoadedEvent += SpawnCombatants;
        }
        
        private void OnDisable()
        {
            GameEvents.OnLoadGameDataEvent -= SetGameData;
            inputManager.onPlayerJoined -= AddPlayer;
            GameEvents.OnFightSceneLoadingEvent -= DoFightSetup;
            GameEvents.OnFightSceneHasLoadedEvent -= SpawnCombatants;
        }

        private void DoFightSetup()
        {
            DisableGamePads();
            inputManager.playerPrefab = fightSceneCharacterPrefab;
            SetPlayerInputFightMode();
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

        public void EnableGamePads()
        {
            foreach (var p in players)
            {
                if(p.GetComponent<GamepadCursor>())
                    p.GetComponent<GamepadCursor>().enabled = true;
            }
        }
        
        void DisableGamePads()
        {
            foreach (var p in players)
            {
                if(p.GetComponent<GamepadCursor>())
                    p.GetComponent<GamepadCursor>().enabled = false;
            }
        }

        void SetPlayerInputUIMode()
        {
            foreach (var p in players)
            {
                p.playerInput.defaultActionMap = UIMap;
            } 
        }

        void SetPlayerInputFightMode()
        {
            foreach (var p in players)
            {
                p.playerInput.defaultActionMap = PlayerMap;
            }
        }
        
        private void SpawnCombatants()
        {
            foreach (var p in players)
            {
//                var player = Instantiate(fightSceneCharacterPrefab);
//                player.SetActive(false);
//                var cs = player.GetComponent<CharacterSetup>();
//                cs.PData = p.playerInstanceData;
//                player.SetActive(true);
            }
        }

        
    }
}