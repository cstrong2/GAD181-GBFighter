using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class CharacterSelectUIv2 : MonoBehaviour
    {
        public static CharacterSelectUIv2 Instance;
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private GameObject characterUI;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] List<CharacterUIv2> characterUIList = null;
        private void Start()
        {
            Instance = this;
            grid = GetComponentInChildren<GridLayoutGroup>();
            GenerateCharacterGrid();
        }

        void GenerateCharacterGrid()
        {
            int count = 0;
            characterUIList = new();
            foreach (var character in characterDatabase.charactersList)
            {
                GameObject charGridElement = Instantiate(characterUI, grid.transform);
                
                // We change the name so that we can see it in the editor
                charGridElement.name = "Character " + count++;
                // Get the UI script because we need to add an ID to it.
                var cUI = charGridElement.GetComponent<CharacterUIv2>() ? charGridElement.GetComponent<CharacterUIv2>() : charGridElement.AddComponent<CharacterUIv2>();
                characterUIList.Add(cUI);
                // Add the sprite to the grid
                cUI.charID = character.CharID;
                cUI.charImage.sprite = character.CharImage;
            }
        }
    }
}