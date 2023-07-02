using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateMachine : MonoBehaviour
{
    [SerializeField] private Animator stateMachineAnimator;

    public void OnInteractComplete()
    {
        SetIdle(true);
        SetHasTask(false);
    }

    public void SetIdle(bool state)
    {
        stateMachineAnimator.SetBool("IsIdle", state);
    }

    public void SetWaitState(bool state)
    {
        stateMachineAnimator.SetBool("IsWaiting", state);
    }

    public void SetHasTask(bool state)
    {
        stateMachineAnimator.SetBool("HasTask", state);
    }

    public bool GetHasTask()
    {
        return stateMachineAnimator.GetBool("HasTask");
    }
}
