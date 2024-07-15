using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackInfo
{
    private float _damage;
    private EvasionZoneType _zoneType;

    public float Damage => _damage;
    public EvasionZoneType Zone => _zoneType;

    public EnemyAttackInfo(float damage, EvasionZoneType evasionZoneType)
    {
        _damage = damage;
        _zoneType = evasionZoneType;
    }
}
