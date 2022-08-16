using System;
using Core;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text winnerText;
        [SerializeField] private Button button;
        private bool _isInstanceNotNull;
        private GameManager _gameManager;

        private void Start()
        {
            _isInstanceNotNull = GameManager.Instance != null ? _gameManager = GameManager.Instance : _gameManager = null ;
            SetWinnerText();
            button.interactable = true;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(LoadMainMenu);
        }
    
        private void OnDisable()
        {
            button.onClick.RemoveListener(LoadMainMenu);
        }
    
        private void SetWinnerText()
        {
            if (!_isInstanceNotNull) return;
            string playerName = $"Player {GameManager.Instance.fightState.WinnerID + 1}";
            winnerText.text = playerName + " is the winner!";
        }

        public void LoadMainMenu()
        {
            Debug.Log("Load Main Menu");
            SceneManager.LoadScene(0);
        }
    
    }
}
