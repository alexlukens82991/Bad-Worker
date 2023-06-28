using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private NPC thisNpc;

    public void FindAndInteract(INpcInteractable interactObj)
    {
        StartCoroutine(InteractWhenReady(interactObj));
    }

    IEnumerator InteractWhenReady(INpcInteractable interactObj)
    {
        navAgent.SetDestination(interactObj.GetLocation());

        yield return new WaitUntil(() => navAgent.hasPath);

        print("remaining distance: " + navAgent.remainingDistance);

        yield return new WaitUntil(() => navAgent.remainingDistance < 1);

        interactObj.Interact(thisNpc);
    }    
}
