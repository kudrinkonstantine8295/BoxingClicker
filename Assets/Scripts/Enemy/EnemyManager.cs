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

    private EnemyData _currentEnemy;
    private float _health = 0f;
    private string _name;
    private GameObject _enemyBody;

    public UnityEvent<EnemyData> OnCurrentEnemyUpdated;
    public UnityEvent OnEnemyDied;

    private int _index = 0;
    private int _gameCompletedTimes = 0;

    private void Start()
    {
        InitializeEnemy(_index);
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
    }

    private void SwitchToNextEnemy()
    {
        _index++;

        if (_index >= _mainEnemiesData.EnemiesDataList.Count + _gameCompletedTimes)
            _index = 0;

            InitializeEnemy(_index);
    }

    private void UpdateHealth(int damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {
            SwitchToNextEnemy();
        }
    }

}
