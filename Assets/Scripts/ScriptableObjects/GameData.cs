using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameData/New GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [Range(1,4)]
        [SerializeField] private int maxPlayers;
        [SerializeField] private int roundTimeInSeconds;
        public int MaxPlayers => maxPlayers;
        
    }
}