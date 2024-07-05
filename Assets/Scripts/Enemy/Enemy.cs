using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private EnemyManager _enemyManager;

    public void Init(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
    }


}
