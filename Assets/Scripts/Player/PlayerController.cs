using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public int playerID; // This will be set by the playerManager
        [SerializeField] private CharacterData currentCharacter = null; // This will be set by the player from the character select menu
    }
}