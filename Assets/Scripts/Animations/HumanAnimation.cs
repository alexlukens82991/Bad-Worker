using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAnimation : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private AnimationInterface animationInterface;

    private void Update()
    {
        WalkCheck();
    }

    private void WalkCheck()
    {
        if (!navAgent.enabled)
        {
            animationInterface.SetIsWalking(false);
            return;
        }

        animationInterface.SetIsWalking(navAgent.remainingDistance > navAgent.stoppingDistance);
    }

}
