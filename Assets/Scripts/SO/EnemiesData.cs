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

    public bool IsStatusIndexValid(int index)
    {
        return (EnemiesDataList[index] != null);
    }
}
