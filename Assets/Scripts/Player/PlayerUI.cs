using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private HealthBarSlider _healthBarSlider;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _health;

    private void OnEnable()
    {
        _playerManager.OnPlayerHealthChanged.AddListener(UpdateHealth);
        _playerManager.OnNameChanged.AddListener(UpdateName);
        
    }

    private void OnDisable()
    {
        _playerManager.OnPlayerHealthChanged.RemoveListener(UpdateHealth);
        _playerManager.OnNameChanged.RemoveListener(UpdateName);
    }

    private void UpdateHealth(float health, float maxHealth)
    {
        _healthBarSlider.ChangeHealthSliderPosition(health, maxHealth);
        _health.text = health.ToString();
        _shaker.StartShaking();
    }

    private void UpdateName(string name)
    {
        _name.text = name;
    }
}
