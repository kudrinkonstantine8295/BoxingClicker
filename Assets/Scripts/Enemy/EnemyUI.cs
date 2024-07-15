using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private HealthBarSlider _healthBarSliderLeft;
    [SerializeField] private HealthBarSlider _HealthBarSliderRight;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private TMP_Text _currentHP;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _zeroHPAlpha = 0.7f;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private Scaler _scaler;

    private float _currentEnemyMaxHealth = 0f;

    private void OnEnable()
    {
        _enemyManager.OnCurrentEnemyUpdated.AddListener(UpdateMaxHealth);
        _enemyManager.OnCurrentEnemyUpdated.AddListener(UpdateName);
        _enemyManager.OnCurrentEnemyHealthChanged.AddListener(UpdateCurrentHealth);
    }

    private void OnDisable()
    {
        _enemyManager.OnCurrentEnemyUpdated.RemoveListener(UpdateMaxHealth);
        _enemyManager.OnCurrentEnemyUpdated.RemoveListener(UpdateName);
        _enemyManager.OnCurrentEnemyHealthChanged.RemoveListener(UpdateCurrentHealth);
    }

    private void UpdateMaxHealth(EnemyData enemyData)
    {
        _currentEnemyMaxHealth = enemyData.Health;
    }

    private void UpdateCurrentHealth(float health)
    {
        _shaker.StartShaking();
        _scaler.StartScaling();
        _healthBarSliderLeft.ChangeHealthSliderPosition(health, _currentEnemyMaxHealth);
        _HealthBarSliderRight.ChangeHealthSliderPosition(health, _currentEnemyMaxHealth);
        _currentHP.text = health.ToString();

        if (health <= 0)
            _canvasGroup.alpha = _zeroHPAlpha;
        else
            _canvasGroup.alpha = 1f;

    }

    private void UpdateName(EnemyData enemyData)
    {
        _name.text = enemyData.Name;
    }
}
