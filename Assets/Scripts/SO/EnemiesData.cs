using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData")]
[Serializable]
public class EnemiesData : ScriptableObject
{
    [field: SerializeField] public List<EnemyData> EnemiesDataList { get; private set; }

    [field: SerializeField] public EnemyStatuses EnemyStatus { get; private set; }

    public bool IsStatusIndexValid(/*EnemyStatusIndex enemyStatusindex*/ int index)
    {
        return (/*enemyStatusindex.Status == EnemyStatus && */EnemiesDataList[/*enemyStatusindex.Index*/ index] != null);
    }
}
