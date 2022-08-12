using System.Collections.Generic;
using Attributes;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Core
{
    public class PlayersManagerInterim : MonoBehaviour
    {
        // TODO: This code will be deleted and the PlayersManager will take over
        
        public static PlayersManagerInterim PMInstance = null;

        [SerializeField] private PlayerInputManager inputManager;
        [SerializeField] [ReadOnly] private List<PlayerInput> players;
        [SerializeField] private CharacterDatabase characterDB;
        [SerializeField] private List<Transform> spawnLocations;

        public List<Transform> SpawnLocations => spawnLocations;
        
        private void Awake()
        {
            inputManager = GetComponent<PlayerInputManager>();
            
            if (PMInstance == null)
            {
                PMInstance = this;
                DontDestroyOnLoad(this.gameObject);
            } else if (PMInstance != null)
            {
                Destroy(this.gameObject);
            }

        }

        private void OnEnable()
        {
            inputManager.onPlayerJoined += AddPlayer;
        }
        
        private void OnDisable()
        {
            inputManager.onPlayerJoined -= AddPlayer;
        }

        private void AddPlayer(PlayerInput player)
        {
            Debug.Log("AddPlayerRan");
            if (players.Find(p => p == player))
            {
                return;
            } else {
                players.Add(player);
            }
            // Set a random character from the db to the player
            var cSetup = player.GetComponent<CharacterSetup>();
//            cSetup.CData = GameManager.Instance.GetCharByID(Random.Range(0, characterDB.charactersList.Count - 1));
            cSetup.PlayerID = player.playerIndex;
        }
    }
}