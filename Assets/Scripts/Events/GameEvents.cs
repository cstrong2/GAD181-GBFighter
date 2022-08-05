
using System.Collections.Generic;

namespace Events
{
    public class GameEvents
    {

        public delegate void SetUpPlayerUI(List<PlayerBrain> players);
        public delegate void CharacterSelected(int charId, int playerId);
        public delegate void AddPlayer();
        public static SetUpPlayerUI OnUISetUpEvent;
        public static CharacterSelected OnPlayerSelectCharacter;
        public static AddPlayer OnAddNewPlayerEvent;

    }
}