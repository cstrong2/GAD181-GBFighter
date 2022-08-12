using System.Collections.Generic;
using System.Linq;
using Core;
using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class JoinGameUI : MonoBehaviour
    {
        [SerializeField]
        private Transform playerPortraitsParent;
        
        [SerializeField]
        private GameObject playerPositionPrefab;
        
        [SerializeField]
        private List<PlayerUIState> playerUIStates;

        [SerializeField] private Button readyButton;

        private void OnEnable()
        {
            GameEvents.OnLoadGameDataEvent += CreatePlayerPositions;
            GameEvents.OnNewPlayerJoinedEvent += ActivatePlayerPosition;
            GameEvents.OnPlayerSelectCharacter += SetPlayerImage;
            readyButton.onClick.AddListener(() => GameEvents.OnAllPlayersReadyEvent?.Invoke());
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
                Instantiate(playerPositionPrefab, playerPortraitsParent);
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
            DeterminePlayersReady();
        }
        
        private void SetPlayerImage(int charID, int playerID)
        {
            var player = playerUIStates[playerID];
            CharacterData data = GameManager.Instance.GetCharByID(charID);
            player.image.sprite = data.CharImage;
            player.characterLabel.text = data.Name;
            player.playerReady = true;
            DeterminePlayersReady();
        }

        void DeterminePlayersReady()
        {
            if(playerUIStates.All(p => p.State == Toggle.Inactive))
            {
                readyButton.interactable = false;
            }
            else if (playerUIStates.Where(p => p.State == Toggle.Active).All(rp => rp.playerReady))
            {
                readyButton.interactable = true;
            }
        }

    }
}