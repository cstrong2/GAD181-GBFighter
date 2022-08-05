
namespace Events
{
    public class GameEvents
    {

        public delegate void SetUpPlayerUI();

        public delegate void CharacterSelected(int charId, int playerId);
        
        public static SetUpPlayerUI OnUISetUpEvent;
        public static CharacterSelected OnPlayerSelectCharacter;

    }
}