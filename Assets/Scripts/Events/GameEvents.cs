
using System.Collections.Generic;
using Player;

namespace Events
{
    public class GameEvents
    {

        public delegate void SetUpPlayerUI(List<PlayerInstance> players);
        public delegate void SendMaxPlayers(int maxPlayers);
        public delegate void CharacterSelected(int charId, int playerId);
        public delegate void NewPlayerJoined(int playerId);
        public delegate void FightSceneLoading();
        public delegate void FightSceneHasLoaded();
        public delegate void AllPlayersReady();
        
        public static SetUpPlayerUI OnUISetUpEvent;
        public static NewPlayerJoined OnNewPlayerJoinedEvent;
        public static CharacterSelected OnPlayerSelectCharacter;
        public static SendMaxPlayers OnLoadGameDataEvent;
        public static AllPlayersReady OnAllPlayersReadyEvent;
        public static FightSceneLoading OnFightSceneLoadingEvent;
        public static FightSceneHasLoaded OnFightSceneHasLoadedEvent;
        
    }
}