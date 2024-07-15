using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Collider> _colliders;

    private EnemyManager _enemyManager;

    public void Init(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
    }

    public void ChangeCollidersInteractionStatus(bool isEnable)
    {
        foreach (var collider in _colliders)
        {
            if (isEnable)
                collider.enabled = true;
            else
                collider.enabled = false;
        }
    }

    public void PlayPunchTakenAnimation(PunchZone punchZone)
    {
        if (punchZone.ZoneType == ZoneType.Top)
        {
            if (Random.Range(0, 2) == 0)
                _animator.Play("LeftPunchTaken");
            else
                _animator.Play("RightPunchTaken");
        }
        else if (punchZone.ZoneType == ZoneType.Bottom)
        {
            if (Random.Range(0, 2) == 0)
                _animator.Play("BellyPunchTaken");
            else
                _animator.Play("BellyPunchTaken2");
        }
    }

    public void CallNextEnemy()
    {
        _enemyManager.SwitchToNextEnemy();
    }

    public void PlayDefeat()
    {
        _animator.Play("Defeat");
    }

    public void PlayKnockDown()
    {
        _animator.Play("KnockDown");
    }
}
