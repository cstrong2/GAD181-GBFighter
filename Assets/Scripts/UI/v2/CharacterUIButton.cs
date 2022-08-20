using System;
using Attributes;
using Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace UI
{
    public class CharacterUIButton : MonoBehaviour
    {
        [SerializeField] public int charID;
        [SerializeField] private Button button;
        [SerializeField] [ReadOnly] private int pIndex;

        private void OnEnable()
        {
            if (button == null)
                button = GetComponentInChildren<Button>();
            
            pIndex = GetComponentInParent<PlayerInput>().playerIndex;
            button.onClick.AddListener(SendCharacterSelection);

        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(SendCharacterSelection);
        }

        void SendCharacterSelection()
        {
            Debug.Log("SendCharacterSelection ran");
            GameEvents.OnPlayerSelectCharacter?.Invoke(charID, pIndex);
        }
    }
}