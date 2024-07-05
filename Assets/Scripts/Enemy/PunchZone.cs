using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchZone : MonoBehaviour
{
    private float _damageTaken = 0f;

    public float DamageTaken => _damageTaken;

    public void AddTakenDamage(float damageTaken)
    {
        _damageTaken += damageTaken;
    }
}
