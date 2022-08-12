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

        public GameData GameData
        {
            get => gameData;
            private set => gameData = value;
        }

        [SerializeField] private string fightScene;
        [SerializeField] private string characterSelectScene;


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

        public void LoadCharacterSelectScreen()
        {
            SceneManager.LoadScene(characterSelectScene);
        }
        
    }
}