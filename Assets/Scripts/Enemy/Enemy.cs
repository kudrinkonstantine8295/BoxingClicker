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
        ChangeCollidersInteractionStatus(false);
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

}
