using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackInfo
{
    private float _damage;
    private Direction _zoneType;

    public float Damage => _damage;
    public Direction Zone => _zoneType;

    public EnemyAttackInfo(float damage, Direction evasionZoneType)
    {
        _damage = damage;
        _zoneType = evasionZoneType;
    }
}
