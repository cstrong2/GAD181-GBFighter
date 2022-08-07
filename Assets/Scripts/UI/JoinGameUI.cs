using System.Collections.Generic;
using System.Linq;
using Core;
using Events;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class JoinGameUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPositionPrefab;

        [SerializeField]
        private List<PlayerUIState> playerUIStates;
        
        private void OnEnable()
        {
            GameEvents.OnLoadGameDataEvent += CreatePlayerPositions;
            GameEvents.OnNewPlayerJoinedEvent += ActivatePlayerPosition;
            GameEvents.OnPlayerSelectCharacter += SetPlayerImage;
        }

        private void OnDisable()
        {
            GameEvents.OnLoadGameDataEvent -= CreatePlayerPositions;
            GameEvents.OnNewPlayerJoinedEvent -= ActivatePlayerPosition;
            GameEvents.OnPlayerSelectCharacter -= SetPlayerImage;
        }
        
        private void CreatePlayerPositions(int maxPlayers)
        {
            for (int i = 0; i < maxPlayers; i++)
            {
                Instantiate(playerPositionPrefab, transform);
            }
            playerUIStates = GetComponentsInChildren<PlayerUIState>().ToList();
            int playerCount = 0;
            foreach (var playerUI in playerUIStates)
            {
                playerUI.AssignedPlayerID = playerCount++;
            }
        }
        
        private void ActivatePlayerPosition(int playerID)
        {
            playerUIStates[playerID].State = Toggle.Active;
        }
        
        private void SetPlayerImage(int charID, int playerID)
        {
            var player = playerUIStates[playerID];
            CharacterData data = GameManager.Instance.GetCharByID(charID);
            player.image.sprite = data.CharImage;
            player.characterLabel.text = data.Name;
        }
        
    }
}