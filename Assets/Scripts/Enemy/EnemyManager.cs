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
    private Enemy _enemyBody;
    private int _currentStage = 0;
    private PunchZoneManager _punchZoneManager = new();
    private bool _isEnemyChanged = false;

    public UnityEvent<EnemyData> OnCurrentEnemyUpdated;
    //public UnityEvent OnEnemyDied;
    public UnityEvent<SaveLoadData> OnSaveLoadStatsChanged;
    public UnityEvent<float> OnCurrentEnemyHealthChanged;

    private const float _percentageMultiplier = 100f;

    private void Start()
    {
        InitializeEnemy(_saveLoadData.Index);
    }

    public void InitializeEnemy(int index)
    {
        GameObject enemyGameObject;

        if (_enemyBody != null)
        {
            Destroy(_enemyBody.gameObject);
            _enemyBody = null;
        }

        if (index < _mainEnemiesData.EnemiesDataList.Count)
            _currentEnemy = _mainEnemiesData.EnemiesDataList[index];
        else
            _currentEnemy = _bonusEnemiesData.EnemiesDataList[index - (_mainEnemiesData.EnemiesDataList.Count)];

        _currentEnemyHealth = Mathf.Round(_currentEnemy.Health * (_currentEnemy.Stages[_currentStage].HealthPercentage / _percentageMultiplier));
        enemyGameObject = Instantiate(_currentEnemy.GameObject, _enemyMovement.transform);

        if (enemyGameObject.TryGetComponent(out Enemy enemy))
        {
            _enemyBody = enemy;
            _enemyBody.Init(this);
        }

        OnCurrentEnemyUpdated?.Invoke(_currentEnemy);
        OnCurrentEnemyHealthChanged?.Invoke(_currentEnemyHealth);
    }

    private bool TrySwitchToNextStage()
    {
        bool isNextStageExist = false;
        _currentStage++;

        if (_currentStage >= _currentEnemy.Stages.Count)
        {
            _currentStage = 0;
        }
        else
        {
            _currentEnemyHealth = Mathf.Round((_currentEnemy.Stages[_currentStage].HealthPercentage / _percentageMultiplier) * _currentEnemy.Health);
            isNextStageExist = true;
        }

        return isNextStageExist;
    }

    public void SwitchToNextEnemy()
    {
        _saveLoadData.Index++;

        if (_saveLoadData.Index >= _mainEnemiesData.EnemiesDataList.Count + _saveLoadData.GameCompletedTimes)
        {
            _saveLoadData.Index = 0;
            _saveLoadData.GameCompletedTimes += 1;
        }

        _isEnemyChanged = true;
        InitializeEnemy(_saveLoadData.Index);
        OnSaveLoadStatsChanged?.Invoke(_saveLoadData);
    }

    public PunchData TakePunch(PunchZone punchZone, SaveLoadData saveLoadData)
    {
        PunchData punchData;

        if (_isEnemyChanged)
            punchData = _punchZoneManager.GetPunchData(punchZone, saveLoadData, true);
        else
            punchData = _punchZoneManager.GetPunchData(punchZone, saveLoadData, false);

        _isEnemyChanged = false;
        _currentEnemyHealth -= punchData.Damage;

        if (_currentEnemyHealth <= 0)
        {
            _currentEnemyHealth = 0;

            if (TrySwitchToNextStage())
                _enemyBody.PlayKnockDown();
            else
                _enemyBody.PlayDefeat();
        }
        else
        {
            _enemyBody.PlayPunchTakenAnimation(punchZone);
        }

        OnCurrentEnemyHealthChanged?.Invoke(_currentEnemyHealth);

        return punchData;
    }

    public EnemyAttackInfo AttackPlayer(List<PlayerEvasionZone> playerEvasionZones)
    {
        var evasionChoise = playerEvasionZones[UnityEngine.Random.Range(0, playerEvasionZones.Count)];
        
        return new EnemyAttackInfo(UnityEngine.Random.Range(_currentEnemy.MinDamage, _currentEnemy.MinDamage), evasionChoise.EvasionZoneType);
    }

    private void DisableEnemyColliders()
    {

    }

}
