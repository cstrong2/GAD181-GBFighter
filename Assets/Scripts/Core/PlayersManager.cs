using System.Collections.Generic;
using System.Linq;
using Attributes;
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
        [SerializeField] private List<PlayerInstance> players;
        [SerializeField] private List<PlayerInput> playerInputs;

        [SerializeField] private List<Transform> spawnPositions;
        [SerializeField] [ReadOnly] private int maxPlayers;
        private bool startUpRan = false;
        
//        [SerializeField] private GameObject fightSceneCharacterPrefab;

        private const string PlayerMap = "Player", UIMap = "UI";

        public int MaxPlayers
        {
            get => maxPlayers;
            set => maxPlayers = value;
        }
        public List<PlayerInstance> Players
        {
            get => players;
            private set => players = value;
        }
        
        public List<PlayerInput> PlayerInputs
        {
            get => playerInputs;
            private set => playerInputs = value;
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
            SetPlayerInputFightMode();
            inputManager.DisableJoining();
            
        }

        private void AddPlayer(PlayerInput player)
        { 
            if (players.Count == maxPlayers)
                return;
//            // We need a message to show if max players is reached or something?

            if (playerInputs.Find(p => p == player))
                return;

            PlayerInputs.Add(player);

            // This sets up the player data for a new player
            var playerInstance = player.GetComponent<PlayerInstance>();
            Players.Add(playerInstance);
            SetUpPlayerData(playerInstance, player);
            Debug.Log(players.Count +" players count");
            
            GameEvents.OnNewPlayerJoinedEvent?.Invoke(player.playerIndex);
        }

        void SetGameData(int maxPlayers)
        {
            if(startUpRan == false)
            {
                // We need to add one player because there will always be at least one.
                players.Capacity = maxPlayers;
                startUpRan = true;
                MaxPlayers = maxPlayers;
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

            foreach (var i in playerInputs)
            {
                i.SwitchCurrentActionMap(UIMap);
            } 
        }

        void SetPlayerInputFightMode()
        {
            foreach (var i in playerInputs)
            {
                i.SwitchCurrentActionMap(PlayerMap);
                Debug.Log(i.currentActionMap);
            }
        }
        
        private void SpawnCombatants()
        {
            GetSpawnLocations();
            foreach (var p in players)
            {
//                var player = Instantiate(fightSceneCharacterPrefab);
                var cs = p.GetComponent<CharacterSetup>();
                cs.playerInput = p.playerInput;
                cs.transform.parent = p.transform;
                cs.SpawnPosition = spawnPositions[p.playerInstanceData.PlayerID];
                cs.PData = p.playerInstanceData;
                cs.CData = GameManager.Instance.GetCharByID(p.playerInstanceData.CurrentCharacterID);
                Debug.Log(p + " has been iterated on");

                p.GetComponent<TopDownController>().enabled = true;
                p.GetComponent<CharacterController>().enabled = true;
                p.GetComponent<Animator>().enabled = true;
                cs.enabled = true;

            }
        }

        private void GetSpawnLocations()
        {
            GameObject spawnLocationsGo = GameObject.Find("SpawnLocations");
            if (!spawnLocationsGo) return;
            spawnPositions = spawnLocationsGo.GetComponentsInChildren<Transform>().ToList();
            spawnPositions.RemoveAt(0);
        }

        private void SetUpPlayerData(PlayerInstance playerInstance, PlayerInput player)
        {
            var pData = ScriptableObject.CreateInstance<PlayerData>();
            pData.PlayerID = player.playerIndex;
            pData.name = pData.PlayerLabel;
            playerInstance.name = pData.PlayerLabel;
            playerInstance.playerInstanceData = pData;
            playerInstance.transform.parent = this.transform;
        }
    }
}