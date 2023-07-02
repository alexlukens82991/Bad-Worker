using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInterface : MonoBehaviour
{
    [Header("Status")]
    public bool IsSitting;

    [Header("Bool Names")]
    [SerializeField] private string isSus;
    [SerializeField] private string isWalking;

    [Header("Trigger Names")]
    [SerializeField] private string sitDown;
    [SerializeField] private string standUp;

    [Header("Cache")]
    [SerializeField] private Animator animator;

    public void StandUpIfSitting()
    {
        if (IsSitting)
            StandUp();
    }

    public void SetIsWalking(bool state)
    {
        animator.SetBool(isWalking, state);
    }

    public void SitDown()
    {
        IsSitting = true;
        animator.SetTrigger(sitDown);
    }

    public void StandUp()
    {
        IsSitting = false;
        animator.SetTrigger(standUp);
    }
}
