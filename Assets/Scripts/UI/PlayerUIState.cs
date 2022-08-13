using Core;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUIState : MonoBehaviour
    { 
        
        [SerializeField] private GameObject playerActive;
        [SerializeField] private GameObject playerInactive;
        protected PlayerInput playerInput;
        private JoinGameUI parentUI;
        [SerializeField] private Toggle state;
        
        
        [Header("Assigned Player settings")]
        public int assignedPlayerID;
        [SerializeField] private TMP_Text playerIDText;
        
        [Header("Character Selection Settings")]
        public TMP_Text characterLabel;
        public Image image;
        public bool playerReady = false;
        public Toggle State
        {
            get => state;
            set { 
                state = value;
                SetToggleState(state);
            }
        }

        public int AssignedPlayerID
        {
            get => assignedPlayerID;
            set
            {
                assignedPlayerID = value;
                playerIDText.text = "P" + (value+1);
            } 
        }

        private void Awake()
        {
            playerReady = false;
            parentUI = GetComponentInParent<JoinGameUI>();
        }

        private void Update()
        {
            SetToggleState(State);
        }

        public void SetToggleState(Toggle newState)
        {
            switch (newState)
            {
                case Toggle.Active : 
                    playerActive.SetActive(true);
                    playerInput = PlayerInput.GetPlayerByIndex(AssignedPlayerID);
                    var action = playerInput.actions.FindAction("Navigate");
                    action.performed += TheThing;
                    playerInactive.SetActive(false);
                    break;
                case Toggle.Inactive: 
                    playerInactive.SetActive(true);
                    playerActive.SetActive(false);
                    break;
            }
        }
        
        private void TheThing(InputAction.CallbackContext obj)
        {
            PlayerInstance player = PlayersManager.Instance.Players[AssignedPlayerID];
            var direction = obj.ReadValue<Vector2>();
            Debug.Log($"PlayerInput is {playerInput.playerIndex} and we are navigating, the phase is {obj.phase} and value is {obj.ReadValue<Vector2>()}");
            if (direction.x < -.5f)
            {
                player.playerInstanceData.CurrentCharacterID -= 1;
                if (player.playerInstanceData.CurrentCharacterID < 0) player.playerInstanceData.CurrentCharacterID = 0;
                Debug.Log("Left");
            } else if (direction.x > .5f)
            {
                player.playerInstanceData.CurrentCharacterID += 1;
                if (player.playerInstanceData.CurrentCharacterID > 3) player.playerInstanceData.CurrentCharacterID = 3;
                Debug.Log("Right");
            }  
            parentUI.SetPlayerImage(player.playerInstanceData.CurrentCharacterID, player.playerInstanceData.PlayerID);

        }
    }
}