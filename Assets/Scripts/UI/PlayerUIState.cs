using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUIState : MonoBehaviour
    { 
        
        [SerializeField] private GameObject playerActive;
        [SerializeField] private GameObject playerInactive;

        [SerializeField] private Toggle state;
        
        
        [Header("Assigned Player settings")]
        public int assignedPlayerID;
        [SerializeField] private TMP_Text playerIDText;
        
        [Header("Character Selection Settings")]
        public TMP_Text characterLabel;
        public Image image;
        public bool playerReady = false;
        public Toggle State
        {
            get => state;
            set { 
                state = value;
                SetToggleState(state);
            }
        }

        public int AssignedPlayerID
        {
            get => assignedPlayerID;
            set
            {
                assignedPlayerID = value;
                playerIDText.text = "P" + (value+1);
            } 
        }

        private void Awake()
        {
            playerReady = false;
        }

        private void Update()
        {
            SetToggleState(State);
        }

        public void SetToggleState(Toggle newState)
        {
            switch (newState)
            {
                case Toggle.Active : 
                    playerActive.SetActive(true);
                    playerInactive.SetActive(false);
                    break;
                case Toggle.Inactive: 
                    playerInactive.SetActive(true);
                    playerActive.SetActive(false);
                    break;
            }
        }

       
    }
}