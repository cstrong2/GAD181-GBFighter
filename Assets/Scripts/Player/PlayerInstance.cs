using Events;
using ScriptableObjects;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerInstance : MonoBehaviour
    {
        public PlayerData playerInstanceData;
        public PlayerInput playerInput;
        public InputSystemUIInputModule uiInputModule;
        private const string Click = "Click";
//        public GamepadCursor gamepadCursor;
//
//        private const string Gamepad = "Gamepad";

//        private void Awake()
//        {
//            SetCursorState();
//        }

//        private void Update()
//        {
//            SetCursorState();
//        }

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