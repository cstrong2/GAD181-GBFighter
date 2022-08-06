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
        private CharacterData currentCharacter = null; // This will be set by the player from the character select menu
        
        public int PlayerID
        {
            get => playerID;
            set
            {
                playerID = value;
                SetPlayerLabel();
            }
        }

        public string PlayerLabel
        {
            get => playerLabel;
            private set => playerLabel = value;
        }

        void SetPlayerLabel()
        {
            int vanityId = playerID + 1;
            PlayerLabel = "Player " + vanityId;
            playerLabelShort = "P" + vanityId;
        }
    }
}