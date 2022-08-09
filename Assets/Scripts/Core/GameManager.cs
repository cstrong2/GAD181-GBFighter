using System;
using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance = null;

        [SerializeField] private GameData gameData;

        [SerializeField] private string fightScene;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != null)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {
            GameEvents.OnAllPlayersReadyEvent += LoadFightScene;
            SceneManager.sceneLoaded += FightSceneHasLoaded;
        }

        private void OnDisable()
        {
            GameEvents.OnAllPlayersReadyEvent -= LoadFightScene;
            SceneManager.sceneLoaded -= FightSceneHasLoaded;
        }

        private void Start()
        {
            GameEvents.OnLoadGameDataEvent?.Invoke(gameData.MaxPlayers);
        }

        public CharacterData GetCharByID(int id)
        {
            return gameData.characterDB.charactersList.Find(m => m.CharID == id);
        }

        void LoadFightScene()
        {
            SceneManager.LoadScene(fightScene);
            GameEvents.OnFightSceneLoadingEvent?.Invoke();
        }

        void FightSceneHasLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == fightScene)
            {  
                GameEvents.OnFightSceneHasLoadedEvent?.Invoke();
            }
        }
    }
}