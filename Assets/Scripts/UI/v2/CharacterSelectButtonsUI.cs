using System;
using System.Collections.Generic;
using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class CharacterSelectButtonsUI : MonoBehaviour
    {
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private GameObject characterUIButton;
        [SerializeField] List<CharacterUIButton> characterUIList = null;
        [SerializeField] private Button readyButton;
        private void Start()
        {
            grid = GetComponentInChildren<GridLayoutGroup>();
            GenerateCharacterButtonsGrid();
        }

        private void OnEnable()
        {
            GameEvents.OnAllPlayersHaveSelectedCharactersEvent += EnableReadyButton;
            readyButton.onClick.AddListener(ReadyButtonPressed);

        }
        
        private void OnDisable()
        {
            GameEvents.OnAllPlayersHaveSelectedCharactersEvent += EnableReadyButton;
            readyButton.onClick.RemoveListener(ReadyButtonPressed);
        }

        private void EnableReadyButton()
        {
            readyButton.interactable = true;
        }

        void ReadyButtonPressed()
        {
            GameEvents.OnAllPlayersReadyEvent?.Invoke();
        }

        void GenerateCharacterButtonsGrid()
        {
            int count = 0;
            characterUIList = new();
            foreach (var character in characterDatabase.charactersList)
            {
                
                var charGridElement = Instantiate(characterUIButton, grid.transform);
                
                // We change the name so that we can see it in the editor
                charGridElement.name = "CharacterButton " + count++;
                // Get the UI script because we need to add an ID to it.
                var cUI = charGridElement.GetComponent<CharacterUIButton>();
                characterUIList.Add(cUI);
                // Add the sprite to the grid
                cUI.charID = character.CharID;
//                cUI.charImage.sprite = character.CharImage;
            }
        }
    }
}