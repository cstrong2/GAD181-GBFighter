using System;
using Events;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameData/New GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [Header("Game Settings")]
        [Range(1,4)]
        [SerializeField] private int maxPlayers;
        [SerializeField] private int roundTimeInSeconds;
        [Space]
        [Header("Character Database")]
        [SerializeField] public CharacterDatabase characterDB;
        public int MaxPlayers => maxPlayers;

        private void OnEnable()
        {
            GameEvents.OnLoadGameDataEvent?.Invoke(MaxPlayers);
        }
    }
}