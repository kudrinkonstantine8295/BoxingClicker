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

    private float _points = 0f;

    public float Points => _points;
    public ZoneType ZoneType => _zoneType;

    public PunchZone(ZoneType zoneType, float points)
    {
        _zoneType = zoneType;
        _points = points;
    }

    public void RemovePoints(float points)
    {
        _points -= points;

        if (_points < 0f)
            _points = 0f;
    }

    public void AddPoints(float Points)
    {
        _points += Points;
    }
}
