using System.Collections.Generic;
using Events;
using Player;
using UnityEngine;

namespace UI
{
    public class FightUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerFightUIPrefab;
        private void OnEnable()
        {
            GameEvents.OnUISetUpEvent += SetUpPlayerUI;
        }

        private void OnDisable()
        {
            GameEvents.OnUISetUpEvent -= SetUpPlayerUI;
        }

        void SetUpPlayerUI(List<PlayerInstance> listOfPlayers)
        {
            foreach (PlayerInstance player in listOfPlayers)
            {
                
                GameObject playerUI = Instantiate(playerFightUIPrefab, transform);
                playerUI.name += " " + player.gameObject.name;

                if(playerUI.GetComponent<PlayerFightUI>())
                    playerUI.GetComponent<PlayerFightUI>().Player = player.playerInstanceData;
                
                if(playerUI.GetComponent<FollowWorldUI>())
                    playerUI.GetComponent<FollowWorldUI>().lookAtPosition = player.gameObject.transform;
            }
        }
    }
}