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
    [SerializeField] private EmergencyLight _emergencyLight;

    private EnemyData _currentEnemy;
    private PunchZoneManager _punchZoneManager = new();
    private Enemy _enemyBody;
    private float _currentEnemyHealth = 0f;
    private int _currentStage = 0;
    private bool _isEnemyChanged = false;
    private Direction _attackDirection;

    public UnityEvent<EnemyData> OnCurrentEnemyUpdated;
    public UnityEvent<SaveLoadData> OnSaveLoadStatsChanged;
    public UnityEvent<float> OnCurrentEnemyHealthChanged;
    public UnityEvent OnEnemyPunched;

    private const float _percentageMultiplier = 100f;

    private void Start()
    {
        InitializeEnemy(_saveLoadData.Index);
        _emergencyLight.InitializeNewLight(_saveLoadData.EmergencyLightField);
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

    public PunchData TakePunch(PunchZone punchZone, SaveLoadData saveLoadData, float damageBonusMultiplier)
    {
        PunchData punchData;

        if (_isEnemyChanged)
            punchData = _punchZoneManager.GetPunchData(punchZone, saveLoadData, true);
        else
            punchData = _punchZoneManager.GetPunchData(punchZone, saveLoadData, false);

        _isEnemyChanged = false;
        _currentEnemyHealth -= Mathf.Round(punchData.Damage * damageBonusMultiplier);

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

        _emergencyLight.SelectPosition(); ///Test

        OnCurrentEnemyHealthChanged?.Invoke(_currentEnemyHealth);

        return punchData;
    }

    public void PlayPrepareAttackAnimation()
    {
        _enemyBody.PlayPrepareAttackAnimation();
        _enemyMovement.KeepDirection(Direction.Middle);
    }

    public void Attack(List<PlayerEvasionZone> playerEvasionZones)
    {
        _attackDirection = playerEvasionZones[UnityEngine.Random.Range(0, playerEvasionZones.Count)].Direction;

        StartCoroutine(AttackCoroutine(_attackDirection));
    }

    public void Attack()
    {
        _attackDirection = Direction.Middle;
        StartCoroutine(AttackCoroutine(Direction.Middle));
    }

    public void ResumeChangePosition()
    {
        _enemyMovement.ResumeRandomChangePosition();
    }

    public IEnumerator AttackCoroutine(Direction direction)
    {
        _enemyMovement.KeepDirection(direction);

        while (_enemyMovement.CheckEnemyCloseToTarget() == false)
        {
            yield return null;
        }

        _enemyBody.PlayAttackAnimation();
    }

    public EnemyAttackInfo AttackPlayer()
    {
        _emergencyLight.InitializeNewLight(_saveLoadData.EmergencyLightField);

        return new EnemyAttackInfo(UnityEngine.Random.Range(_currentEnemy.MinDamage, _currentEnemy.MinDamage), _attackDirection);
    }

    public void ChangeInteractionStatus(bool isActivate)
    {
        if (_enemyBody != null)
        {
            _enemyBody.ChangeInteractionStatus(isActivate);
        }
    }
}
