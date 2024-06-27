using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private EnemyManager _enemyManager;

    private float _currentEnemyMaxHealth = 0f;

    private void OnEnable()
    {
        _enemyManager.OnCurrentEnemyUpdated.AddListener(UpdateMaxHealth);
        _enemyManager.OnCurrentEnemyHealthChanged.AddListener(UpdateCurrentHealth);
    }

    private void OnDisable()
    {
        _enemyManager.OnCurrentEnemyUpdated.RemoveListener(UpdateMaxHealth);
        _enemyManager.OnCurrentEnemyHealthChanged.RemoveListener(UpdateCurrentHealth);
    }

    private void UpdateMaxHealth(EnemyData enemyData)
    {
        _currentEnemyMaxHealth = enemyData.Health;
    }

    private void UpdateCurrentHealth(float health)
    {
        _image.fillAmount = health/ _currentEnemyMaxHealth;
    }
}
