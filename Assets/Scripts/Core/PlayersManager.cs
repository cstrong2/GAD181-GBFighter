using System.Collections.Generic;
using System.Linq;
using Attributes;
using Events;
using Player;
using ScriptableObjects;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager Instance = null;
        [Space]
        [SerializeField] private GameData gameData;
        [SerializeField] [ReadOnly] private bool startUpRan = false;
        [Space]
        [SerializeField] private PlayerInputManager inputManager;
        [Header("Player References")]
        [SerializeField] private List<PlayerInstance> players;
        [SerializeField] private List<PlayerInput> playerInputs;
        [SerializeField] private List<Transform> spawnPositions;
        [SerializeField] [ReadOnly] private int maxPlayers;

        [SerializeField] private GameObject playerFightPrefab;

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
            SetGameData(gameData.MaxPlayers);
            inputManager.onPlayerJoined += AddPlayer;
            GameEvents.OnFightSceneLoadingEvent += DoFightSetup;
            GameEvents.OnFightSceneHasLoadedEvent += SpawnCombatants;
            GameEvents.OnPlayerSelectedCharacterEvent += SetCharData;
            GameEvents.OnPlayerWonEvent += DeactivateCombatants;

        }
        

        private void OnDisable()
        {
            inputManager.onPlayerJoined -= AddPlayer;
            GameEvents.OnFightSceneLoadingEvent -= DoFightSetup;
            GameEvents.OnFightSceneHasLoadedEvent -= SpawnCombatants;
            GameEvents.OnPlayerSelectedCharacterEvent -= SetCharData;
            GameEvents.OnPlayerWonEvent -= DeactivateCombatants;

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

            if (playerInputs.Find(p => player.playerIndex == p.playerIndex ))
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
                i.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
            } 
        }

        void SetPlayerInputFightMode()
        {
            foreach (var i in playerInputs)
            {
                i.SwitchCurrentActionMap(PlayerMap);
                i.notificationBehavior = PlayerNotifications.SendMessages;
//                Debug.Log(i.currentActionMap);
            }
        }
        
        private void SpawnCombatants()
        {
            GameEvents.OnUISetUpEvent?.Invoke(players);
            GetSpawnLocations();
            foreach (var p in playerInputs)
            {
                
                var playerInstance = p.GetComponent<PlayerInstance>();
                var playerGameObject = PlayerInput.Instantiate(playerFightPrefab, p.playerIndex, controlScheme: p.currentControlScheme);
                var cs = playerGameObject.GetComponent<CharacterSetup>();
                cs.PData = playerInstance.playerInstanceData;
                cs.playerInput = p;
                cs.transform.parent = p.transform;
                cs.SpawnPosition = spawnPositions[p.playerIndex];
                
                cs.CData = gameData.characterDB.GetCharByID(players[p.playerIndex].playerInstanceData.CurrentCharacterID);
                Debug.Log(p + " has been iterated on");

                playerGameObject.GetComponent<TopDownController>().enabled = true;
                playerGameObject.GetComponent<CharacterController>().enabled = true;
                cs.enabled = true;

                Destroy(playerGameObject.GetComponentInParent<Canvas>());

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
//            var cs = playerInstance.GetComponent<CharacterSetup>();
//            cs.PData = pData;
        }
        
        private void SetCharData(int charid, int playerid)
        {
            Debug.Log("CurrentChar ID = " + charid + " from player " + playerid);
//            var character = GameManager.Instance.GetCharByID(charid);
//            players.Find(p => p.playerInstanceData.PlayerID == playerid).GetComponent<CharacterSetup>().CData =
//                character;
        }
        
        private void DeactivateCombatants(int id)
        {
            foreach (var playerInstance in Players)
            {
                playerInstance.GetComponentInChildren<TopDownController>().enabled = false;
                playerInstance.GetComponentInChildren<CharacterController>().enabled = false;
            }
        }
    }
}