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

    public float Damage => _damage;
    public PunchType PunchType => _punchType;

    public PunchData(float damage, PunchType punchType)
    {
        _damage = damage;
        _punchType = punchType;
    }

}
