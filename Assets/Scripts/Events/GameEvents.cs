﻿
using System.Collections.Generic;
using Player;

namespace Events
{
    public class GameEvents
    {

        public delegate void SetUpPlayerUI(List<PlayerInstance> players);

        public delegate void SendMaxPlayers(int maxPlayers);
        public delegate void CharacterSelected(int charId, int playerId);
        public delegate void AddPlayer();
        
        public static SetUpPlayerUI OnUISetUpEvent;
        public static CharacterSelected OnPlayerSelectCharacter;
        public static AddPlayer OnAddNewPlayerEvent;
        public static SendMaxPlayers OnLoadGameDataEvent;

    }
}