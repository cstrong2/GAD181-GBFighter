
namespace Events
{
    public class GameEvents
    {

        public delegate void SetUpPlayerUI();
        
        public static SetUpPlayerUI OnUISetUpEvent;
        
    }
}