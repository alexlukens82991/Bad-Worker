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
        animationInterface.SetIsWalking(navAgent.remainingDistance > navAgent.stoppingDistance);
    }

}
