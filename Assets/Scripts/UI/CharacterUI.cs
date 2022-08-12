using System;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] public int charID;
        [SerializeField] public Image charImage;
        [SerializeField] private Button selectionButton;

        private void Awake()
        {
            if(selectionButton == null)
                selectionButton = GetComponentInChildren<Button>();
            
            if(charImage == null)
            {
                charImage = GetComponentsInChildren<Image>()[0];
            }
            
        }

        private void OnEnable()
        {
            GameEvents.OnPlayerClickedEvent += CharacterSelected;
        }

        void CharacterSelected(int playerID)
        {
            selectionButton.onClick.AddListener(() => GameEvents.OnPlayerSelectCharacter?.Invoke(charID, playerID));
        }
    }
}