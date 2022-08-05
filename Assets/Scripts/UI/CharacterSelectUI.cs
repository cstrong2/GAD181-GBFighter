using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI
{
    public class CharacterSelectUI : MonoBehaviour
    {
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private GameObject characterUI;
        [SerializeField] private GridLayoutGroup grid;
        private void Awake()
        {
            grid = GetComponentInChildren<GridLayoutGroup>();
            GenerateCharacterGrid();
            
//            var eventSystem = EventSystem.current;
//            eventSystem.SetSelectedGameObject( this.gameObject, new BaseEventData(eventSystem));

        }

        void GenerateCharacterGrid()
        {
            int count = 0;
            foreach (var character in characterDatabase.charactersList)
            {
                GameObject charGridElement = Instantiate(characterUI, grid.transform);
                
                // We change the name so that we can see it in the editor
                charGridElement.name = "Character " + count++;
                // Get the UI script because we need to add an ID to it.
                var cUI = charGridElement.GetComponent<CharacterUI>() ? charGridElement.GetComponent<CharacterUI>() : charGridElement.AddComponent<CharacterUI>();
                // Add the sprite to the grid
                cUI.charID = character.CharID;
                cUI.charImage.sprite = character.CharImage;
            }
        }
    }
}