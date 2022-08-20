using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInstance : MonoBehaviour
    {
        public PlayerData playerInstanceData;
        public PlayerInput playerInput;
        private const string Click = "Click";


        private void OnEnable()
        {
//            uiInputModule = GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
//            playerInput.uiInputModule = uiInputModule;
            playerInput.actions.FindAction(Click).performed += PassPlayerInputID;
        }

        void PassPlayerInputID(InputAction.CallbackContext ctx)
        {
            GameEvents.OnPlayerClickedEvent?.Invoke(playerInput.playerIndex);
            Debug.Log($"Player {playerInput.playerIndex} clicked");
        }

//        void SetCursorState()
//        {
//            if (playerInput.currentControlScheme == Gamepad)
//            {
//                gamepadCursor.enabled = true;
//            }
//            else
//            {
//                gamepadCursor.enabled = false;
//            }
//        }
        
    }
    
}