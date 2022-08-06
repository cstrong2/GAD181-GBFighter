using Events;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance = null;

        [SerializeField]
        private GameData gameData;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else if (Instance != null)
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            GameEvents.OnLoadGameDataEvent?.Invoke(gameData.MaxPlayers);
        }
    }
}