using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;

    private EnemyStatusIndex _enemyStatusIndex = new() { Index = 1, Status = EnemyStatuses.Main };

    private void Start()
    {
        _enemyManager.InitializeEnemy(_enemyStatusIndex);
    }
}
