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
        [SerializeField] public FightState fightState;
        public GameData GameData
        {
            get => gameData;
            private set => gameData = value;
        }

        [SerializeField] private string fightScene, characterSelectScene, endScene;


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
            GameEvents.OnPlayerWonEvent += LoadEndScene;
        }

        private void LoadEndScene(int id)
        {
            SceneManager.LoadScene(endScene);
            GameEvents.OnGameOverUIEvent?.Invoke();
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
            
            if(fightState != null)
                DestroyImmediate(fightState);
        }

        void FightSceneHasLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == fightScene)
            {  
                GameEvents.OnFightSceneHasLoadedEvent?.Invoke();
                
                if(fightState == null && PlayersManager.Instance)
                {
                    fightState = ScriptableObject.CreateInstance<FightState>();
                    foreach (var p in PlayersManager.Instance.Players)
                    {
                        bool newBool = new bool();
                        fightState.PlayersDead.Add(newBool);
                    }
                }
            }
        }

        public void LoadCharacterSelectScreen()
        {
            SceneManager.LoadScene(characterSelectScene);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
  
    }
}