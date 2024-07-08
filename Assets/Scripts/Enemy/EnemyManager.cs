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
    [SerializeField] private SaveLoadData _saveLoadData;
    [SerializeField] private EnemyMovement _enemyMovement;

    private EnemyData _currentEnemy;
    private float _currentEnemyHealth = 0f;
    private GameObject _enemyBody;
    private int _currentStage = 0;
    private PunchZoneManager _punchZoneManager = new();
    private bool _isEnemyChanged = false;

    public UnityEvent<EnemyData> OnCurrentEnemyUpdated;
    public UnityEvent OnEnemyDied;
    public UnityEvent<SaveLoadData> OnPlayerStatsChanged;
    public UnityEvent<float> OnCurrentEnemyHealthChanged;

    private const float _percentageMultiplier = 100f;

    private void Start()
    {
        InitializeEnemy(_saveLoadData.Index);
    }

    public void InitializeEnemy(int index)
    {
        if (_enemyBody != null)
        {
            Destroy(_enemyBody);
            _enemyBody = null;
        }

        if (index < _mainEnemiesData.EnemiesDataList.Count)
            _currentEnemy = _mainEnemiesData.EnemiesDataList[index];
        else
            _currentEnemy = _bonusEnemiesData.EnemiesDataList[index - (_mainEnemiesData.EnemiesDataList.Count)];

        _currentEnemyHealth = _currentEnemy.Health * (_currentEnemy.Stages[_currentStage].HealthPercentage / _percentageMultiplier);
        _enemyBody = Instantiate(_currentEnemy.GameObject, _enemyMovement.transform);

        if (_enemyBody.TryGetComponent(out Enemy enemy))
        {
            enemy.Init(this);
        }

        OnCurrentEnemyUpdated?.Invoke(_currentEnemy);
        OnCurrentEnemyHealthChanged?.Invoke(_currentEnemyHealth);
    }

    private void SwitchToNextStage()
    {
        _currentStage++;

        if (_currentStage >= _currentEnemy.Stages.Count)
        {
            _currentStage = 0;
            SwitchToNextEnemy();
            _isEnemyChanged = true;
        }
        else
        {
            _currentEnemyHealth = (_currentEnemy.Stages[_currentStage].HealthPercentage / _percentageMultiplier) * _currentEnemy.Health;
        }
    }

    private void SwitchToNextEnemy()
    {
        _saveLoadData.Index++;

        if (_saveLoadData.Index >= _mainEnemiesData.EnemiesDataList.Count + _saveLoadData.GameCompletedTimes)
        {
            _saveLoadData.Index = 0;
            _saveLoadData.GameCompletedTimes += 1;
        }

        InitializeEnemy(_saveLoadData.Index);
        OnPlayerStatsChanged?.Invoke(_saveLoadData);
    }

    public PunchData TakePunch(PunchZone punchZone, SaveLoadData saveLoadData)
    {
        PunchData punchData;

        if (_isEnemyChanged)
            punchData = _punchZoneManager.GetPunchData(punchZone, saveLoadData, true);
        else
            punchData = _punchZoneManager.GetPunchData(punchZone, saveLoadData, false);

        _currentEnemyHealth -= punchData.Damage;

        if (_currentEnemyHealth <= 0f)
        {
            SwitchToNextStage();
        }

        OnCurrentEnemyHealthChanged?.Invoke(_currentEnemyHealth);

        return punchData;
    }

    private void DisableEnemyColliders()
    {

    }

}
