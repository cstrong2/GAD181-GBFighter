using Attributes;
using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class CharacterUISelectRandom : MonoBehaviour
    {
        [SerializeField] private CharacterDatabase charDB;
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
            int _characterID = charDB.GetRandomCharacterID();
            Debug.Log("SendCharacterSelection ran");
            GameEvents.OnPlayerSelectCharacter?.Invoke(_characterID, pIndex);
        }
    }
}