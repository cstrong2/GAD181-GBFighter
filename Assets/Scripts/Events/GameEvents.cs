
using System.Collections.Generic;

namespace Events
{
    public class GameEvents
    {

        public delegate void SetUpPlayerUI(List<PlayerController> players);

        public delegate void CharacterSelected(int charId, int playerId);
        
        public static SetUpPlayerUI OnUISetUpEvent;
        public static CharacterSelected OnPlayerSelectCharacter;

    }
}