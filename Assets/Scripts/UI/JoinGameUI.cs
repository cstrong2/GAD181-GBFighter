using System;
using Events;
using UnityEngine;

namespace UI
{
    public class JoinGameUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPositionPrefab;

        private void OnEnable()
        {
            GameEvents.OnLoadGameDataEvent += CreatePlayerPositions;
        }
        
        private void OnDisable()
        {
            GameEvents.OnLoadGameDataEvent -= CreatePlayerPositions;
        }

        private void CreatePlayerPositions(int maxPlayers)
        {
            for (int i = 0; i < maxPlayers; i++)
            {
                Instantiate(playerPositionPrefab, transform);
            }
        }

        private void Start()
        {
            
        }
    }
}