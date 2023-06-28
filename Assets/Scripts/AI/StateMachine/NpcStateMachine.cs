using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateMachine : MonoBehaviour
{
    [SerializeField] private Animator stateMachineAnimator;

    public void OnInteractComplete()
    {
        stateMachineAnimator.SetBool("IsIdle", true);
    }
}
