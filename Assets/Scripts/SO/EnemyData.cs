using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStatuses
{
    Main,
    Bonus
}

public struct EnemyStatusIndex
{
    public EnemyStatuses Status;
    public int Index;
}

public struct Stage
{
    public AudioClip Clip;
    public Color Color;
    public uint HealthPercentage;
}

[Serializable]
public class EnemyData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float MinDamage { get; private set; }
    [field: SerializeField] public float MaxDamage { get; private set; }
    [field: SerializeField] public float Money { get; private set; }
    [field: SerializeField] public List<Stage> Stages { get; private set; }
    [field: SerializeField] public Sprite _mainIcon { get; private set; }
    [field: SerializeField] public GameObject GameObject { get; private set; }
}
