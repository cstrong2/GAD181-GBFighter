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
        [SerializeField] private FightState fightState;
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
            
            if(fightState != null)
                DestroyImmediate(fightState);
            
            fightState = ScriptableObject.CreateInstance<FightState>();
            foreach (var p in PlayersManager.Instance.Players)
            { 
                fightState.PlayersDead.Add(false);
            }
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
        
        // foreach player, we need to add a bool to the list in the order of the players.
        // When a player hp is 0, we need to send an event that passes the ID of the player
        // and we set the bool to true for that id in the list.
        // we then do a linq except of true, and if that new list only has one member then the game is over.
        
    }
}