using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider1;
    [SerializeField] private Slider _healthSlider2;
    [SerializeField] private Slider _smoothSlider1;
    [SerializeField] private Slider _smoothSlider2;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private TMP_Text _currentHP;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _zeroHPAlpha = 0.7f;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private Scaler _scaler;
    [SerializeField] private float _smoothSpeed;

    private float _currentEnemyMaxHealth = 0f;
    private float _currentHealthSLider = 0f;

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
        _currentHealthSLider = health / _currentEnemyMaxHealth;
        _healthSlider1.value = _currentHealthSLider;
        _healthSlider2.value = _currentHealthSLider;
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

    private void TrySmoothSliderHealthBar()
    {
        if (_smoothSlider1.value != _currentHealthSLider)
        {
            if (_smoothSlider1.value < _currentHealthSLider)
                _smoothSlider1.value = _currentHealthSLider;
            else
            {
                _smoothSlider1.value = Mathf.MoveTowards(_smoothSlider1.value, _currentHealthSLider, Time.deltaTime * _smoothSpeed);
                _smoothSlider2.value = Mathf.Lerp(_smoothSlider1.value, _currentHealthSLider, Time.deltaTime * _smoothSpeed);
            }

        }
    }

    private void Update()
    {
        TrySmoothSliderHealthBar();
    }
}
