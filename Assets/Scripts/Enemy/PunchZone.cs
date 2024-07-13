using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZoneType
{
    None,
    Top,
    Bottom,
}

public class PunchZone : MonoBehaviour
{
    [SerializeField] private ZoneType _zoneType;

    private float _damageTaken = 0f;

    public float DamageTaken => _damageTaken;
    public ZoneType ZoneType => _zoneType;

    public void AddTakenDamage(float damageTaken)
    {
        _damageTaken += damageTaken;
    }
}
