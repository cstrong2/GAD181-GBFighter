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
            selectionButton.onClick.AddListener(CharacterSelected);
        }

        void CharacterSelected()
        {
            GameEvents.OnPlayerSelectCharacter?.Invoke(charID, 0); // how do we get this player id?
        }
    }
}