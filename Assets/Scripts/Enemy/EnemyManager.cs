using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemiesData _mainEnemiesData;
    [SerializeField] private EnemiesData _bonusEnemiesData;
    [SerializeField] private Transform _enemyParent;
    [SerializeField] private PlayerStats _playerStats;

    private EnemyData _currentEnemy;
    private float _currentEnemyHealth = 0f;
    private GameObject _enemyBody;

    public UnityEvent<EnemyData> OnCurrentEnemyUpdated;
    public UnityEvent OnEnemyDied;
    public UnityEvent<PlayerStats> OnPlayerStatsChanged;

    private void Start()
    {
        InitializeEnemy(_playerStats.Index);
    }

    private bool TryGetCurrentEnemy(EnemiesData enemiesData, int index)
    {
        if (enemiesData.IsStatusIndexValid(index))
        {
            _currentEnemy = enemiesData.EnemiesDataList[index];
            return true;
        }
        else
            return false;
    }

    public void InitializeEnemy(int index)
    {
        if (_enemyBody != null)
        {
            Destroy(_enemyBody);
        }

        if (index < _mainEnemiesData.EnemiesDataList.Count)
            _currentEnemy = _mainEnemiesData.EnemiesDataList[index];
        else
            _currentEnemy = _bonusEnemiesData.EnemiesDataList[index - (_mainEnemiesData.EnemiesDataList.Count - 1)];

        _currentEnemyHealth = _currentEnemy.Health;

        OnCurrentEnemyUpdated?.Invoke(_currentEnemy);
    }

    private void SwitchToNextEnemy()
    {
        _playerStats.Index++;

        if (_playerStats.Index >= _mainEnemiesData.EnemiesDataList.Count + _playerStats.GameCompletedTimes)
        {
            _playerStats.Index = 0;
        }

        InitializeEnemy(_playerStats.Index);
        OnPlayerStatsChanged?.Invoke(_playerStats);
    }

    private void UpdateHealth(int damage)
    {
        _currentEnemyHealth -= damage;

        if (_currentEnemyHealth <= 0f)
        {
            SwitchToNextEnemy();
        }
    }

}
