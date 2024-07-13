using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out Enemy enemy))
            enemy.ChangeCollidersInteractionStatus(false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out Enemy enemy))
            enemy.ChangeCollidersInteractionStatus(true);
    }
}
