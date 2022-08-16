
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Events
{
    public class GameEvents
    {

        public delegate void SendMaxPlayers(int maxPlayers);
        public delegate void CharacterSelect(int charId, int playerId);
        public delegate void NewPlayerJoined(int playerId);
        public delegate void FightSceneLoading();
        public delegate void FightSceneHasLoaded();
        public delegate void AllPlayersReady();
        public delegate void PlayerClicked(int playerId);
        public delegate void SetUpPlayerUI(List<PlayerInstance> players);
        public delegate void SendCharacterDamaged(int playerID, float normalisedValue);
        public delegate void PlayerDied(int playerID);

        public delegate void PlayerWon(int id);
        
        public static NewPlayerJoined OnNewPlayerJoinedEvent;
        public static CharacterSelect OnPlayerSelectCharacter;
        public static CharacterSelect OnPlayerSelectedCharacterEvent;
        public static SendMaxPlayers OnLoadGameDataEvent;
        public static AllPlayersReady OnAllPlayersReadyEvent;
        public static FightSceneLoading OnFightSceneLoadingEvent;
        public static FightSceneHasLoaded OnFightSceneHasLoadedEvent;
        public static PlayerClicked OnPlayerClickedEvent;
        public static SetUpPlayerUI OnUISetUpEvent;
        public static SendCharacterDamaged OnCharacterDamagedEvent;
        public static PlayerDied OnPlayerDiedEvent;
        public static PlayerWon OnPlayerWonEvent;
    }
}