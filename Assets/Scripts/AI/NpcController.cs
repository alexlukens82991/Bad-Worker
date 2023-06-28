using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    [SerializeField] private NPC thisNpc;

    public void FindAndInteract(INpcInteractable interactObj)
    {
        StartCoroutine(InteractWhenReady(interactObj));
    }

    IEnumerator InteractWhenReady(INpcInteractable interactObj)
    {
        NavAgent.SetDestination(interactObj.GetLocation());

        yield return new WaitUntil(() => NavAgent.hasPath);

        yield return new WaitUntil(() => NavAgent.remainingDistance < 1);

        yield return StartCoroutine(interactObj.Interact(thisNpc));

        thisNpc.OnInteractComplete();
    }    
}
