using System;
using Events;
using UnityEngine;

namespace ScriptableObjects
{
    public class PlayerData : ScriptableObject
    {
        [SerializeField]
        private int playerID; // This will be set by the playerManager
        [SerializeField]
        private string playerLabel, playerLabelShort; // This will be set by the playerManager
        [SerializeField] 
        private int currentCharacterID; // This will be set by the player from the character select menu

        public int PlayerID
        {
            get => playerID;
            set
            {
                playerID = value;
                SetPlayerLabel();
            }
        }
        
        public int CurrentCharacterID
        {
            get => currentCharacterID;
            set
            {
                currentCharacterID = value;
                GameEvents.OnPlayerSelectedCharacterEvent?.Invoke(currentCharacterID, playerID);
            }
        }


        public string PlayerLabel
        {
            get => playerLabel;
            private set => playerLabel = value;
        }

        public string PlayerLabelShort
        {
            get => playerLabelShort;
            private set => playerLabelShort = value;
        }

        private void OnEnable()
        {
            GameEvents.OnPlayerSelectCharacter += AssignCharacter;
        }

        private void OnDisable()
        {
            GameEvents.OnPlayerSelectCharacter -= AssignCharacter;
        }
        
        private void AssignCharacter(int charID, int playerID)
        {
            if (playerID != this.playerID)
                return;
            CurrentCharacterID = charID;
        }

        void SetPlayerLabel()
        {
            int vanityId = PlayerID + 1;
            PlayerLabel = "Player " + vanityId;
            PlayerLabelShort = "P" + vanityId;
        }
        
        
    }
}