using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Shake()
    {
        _animator.SetTrigger("Click");
    }
}
