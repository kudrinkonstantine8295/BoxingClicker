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

    public UnityEvent<EnemyData> OnEnemyDataUpdated;
    public UnityEvent OnEnemyDied;

    private bool TryGetCurrentEnemy(EnemiesData enemiesData, EnemyStatusIndex enemyStatusIndex)
    {
        if (enemiesData.IsStatusIndexValid(enemyStatusIndex))
        {
            _currentEnemy = enemiesData.EnemiesDataList[enemyStatusIndex.Index];
            return true;
        }
        else
            return false;
    }

    public void InitializeEnemy(EnemyStatusIndex enemyStatusIndex)
    {
        if (_enemyBody != null)
        {
            Destroy(_enemyBody);
        }

        if (TryGetCurrentEnemy(_mainEnemiesData, enemyStatusIndex) || TryGetCurrentEnemy(_bonusEnemiesData, enemyStatusIndex))
        {
            OnEnemyDataUpdated?.Invoke(_currentEnemy);
            _name = _currentEnemy.Name;
        }
    }

    private void SwitchToNextEnemy()
    {

    }

    private void UpdateHealth(int damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {

        }
    }

}
