using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PunchType
{
    Usual,
    Crit,
    UltraCrit
}

public class PunchData
{
    private float _damage;
    private PunchType _punchType;
    private ZoneType _zoneType;

    public float Damage => _damage;
    public PunchType PunchType => _punchType;
    public ZoneType ZoneType => _zoneType;

    public PunchData(float damage, PunchType punchType, ZoneType zoneType)
    {
        _damage = damage;
        _punchType = punchType;
        _zoneType = zoneType;
    }
}
