using System.Collections.Generic;
using System.Linq;
using Events;
using UnityEngine;

namespace ScriptableObjects
{
    public class FightState : ScriptableObject
    {
        // foreach player, we need to add a bool to the list in the order of the players.
        // When a player hp is 0, we need to send an event that passes the ID of the player
        // and we set the bool to true for that id in the list.
        // we then do a linq except of true, and if that new list only has one member then the game is over.
        [SerializeField]
        private List<bool> playersDead = new();
        [SerializeField]
        private int winnerID; 
        public int WinnerID
        {
            get => winnerID;
            set => winnerID = value;
        }

        public List<bool> PlayersDead
        {
            get => playersDead;
            set => playersDead = value;
        }

        private void OnEnable()
        {
            GameEvents.OnPlayerDiedEvent += SetPlayerDead;
        }
        
        private void OnDisable()
        {
            GameEvents.OnPlayerDiedEvent -= SetPlayerDead;
        }
        
        private void SetPlayerDead(int playerid)
        {
            PlayersDead[playerid] = true;
            var isDead = PlayersDead.FindAll(b => b.Equals(true));
            Debug.Log($"isDead equals {isDead} & {isDead.Count}");

            var check = PlayersDead.Except(isDead).ToList();
            Debug.Log($"check equals {check} & {check.Count}");
            
            DeterminePlayerWin(check);
            
        }

        void DeterminePlayerWin(List<bool> checkList)
        {
            if (checkList.Count <= 1)
            {
                int winner = PlayersDead.FindIndex(b => b.Equals(false));
                WinnerID = winner;
                GameEvents.OnPlayerWonEvent?.Invoke(winner);
//                Debug.Log($"The player that has won the fight is {winner}");
            }
        }
        
    }
}