using System;
using System.Collections;
using Events;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFightUI : MonoBehaviour
{
    [SerializeField] private PlayerData _player;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private float updateSpeedSeconds = 0.5f;

    [Header("Slider settings")] 
    [SerializeField] private Color sliderWarningColor = Color.red;

    [SerializeField] private float sliderWarningValue = 0.3f;
    
    private TMP_Text _playerLabel;

    public PlayerData Player
    {
        get => _player;
        set
        {
            _player = value;
            _playerLabel = GetComponentInChildren<TMP_Text>();
            _playerLabel.text = Player.PlayerLabelShort;
        }
    }

    private void OnEnable()
    {
        
        if(_hpSlider == null)
            _hpSlider = GetComponentInChildren<Slider>();
        
        GameEvents.OnCharacterDamagedEvent += ChangeSlider;
    }

    private void OnDisable()
    {
        GameEvents.OnCharacterDamagedEvent -= ChangeSlider;
    }

    private void ChangeSlider(int playerID, float normalisedValue)
    {
        if (_player.PlayerID != playerID)
            return;
        
        StartCoroutine(AnimateSlider(normalisedValue));
        
    }

    private IEnumerator AnimateSlider(float normalisedValue)
    {
        Debug.Log("Coroutine is running to animate slider");
        float preChangedPercent = _hpSlider.value;
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            _hpSlider.value = Mathf.Lerp(preChangedPercent, normalisedValue, elapsed / updateSpeedSeconds);
            if (_hpSlider.value <= sliderWarningValue)
                _hpSlider.fillRect.gameObject.GetComponent<Image>().color = sliderWarningColor;
            
            yield return null;
        }
        _hpSlider.value = normalisedValue;
        StopCoroutine("AnimateSlider");
    }
    
}
