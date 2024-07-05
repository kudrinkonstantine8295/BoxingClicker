using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveLoadData")]
[Serializable]
public class SaveLoadData : ScriptableObject
{
    [field: SerializeField] public float MinDamage = 1f;
    [field: SerializeField] public float MaxDamage = 3f;
    [field: SerializeField] public float Money = 0f;
    [field: SerializeField] public float CritMultiplier = 1.5f;
    [field: SerializeField] public int CritChancePercentage = 10;
    [field: SerializeField] public float UltraCritMultiplier = 1.5f;
    [field: SerializeField] public int UltraCritChancePercentage = 10;
    [field: SerializeField] public float SamePunchZoneMaxPenulty = 0.3f;
    [field: SerializeField] public float Heal = 1f;
    [field: SerializeField] public float Health = 100f;
    [field: SerializeField] public int Index = 0;
    [field: SerializeField] public int GameCompletedTimes = 0;

}
