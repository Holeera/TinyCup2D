using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void OnEnableAnimator()
    {
        animator.enabled=true;
    }
}
