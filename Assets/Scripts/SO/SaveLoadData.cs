using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats")]
[Serializable]
public class SaveLoadData : ScriptableObject
{
    [field: SerializeField] public float Damage;
    [field: SerializeField] public float Money;
    [field: SerializeField] public float CritMultiplier;
    [field: SerializeField] public float Heal;
    [field: SerializeField] public float Health;
    [field: SerializeField] public int Index = 0;
    [field: SerializeField] public int GameCompletedTimes = 0;
}
