using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    [SerializeField] private NPC thisNpc;

    [Header("Status")]
    public bool RoamRoutineOn;

    private INpcInteractable currentTargetInteractObj;
    private Coroutine currentFindRoutine;
    private Coroutine currentInteractRoutine;
    private Coroutine CurrentRoamRoutine;


    public void SetDestination(Vector3 target)
    {
        NavAgent.SetDestination(target);
    }

    public void SetRoamRoutineActive(bool active)
    {
        StopAllInteractRoutines();

        if (active)
        {
            CurrentRoamRoutine = StartCoroutine(RoamRoutine());
        }
        else
        {
            RoamRoutineOn = false;
        }
    }

    IEnumerator RoamRoutine()
    {
        RoamRoutineOn = true;

        do
        {
            SetDestination(RandomNavmeshLocation(3));
            yield return new WaitForSeconds(Random.Range(3f, 10f));
        } while (RoamRoutineOn);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void Wait()
    {
        StopAllInteractRoutines();

        NavAgent.SetDestination(transform.position);
    }

    public void OnEndWait()
    {
        if (currentTargetInteractObj == null)
            return;

        FindAndInteract(currentTargetInteractObj);
    }

    public void FindAndInteract(INpcInteractable interactObj)
    {
        thisNpc.StateMachine.SetHasTask(true);

        currentTargetInteractObj = interactObj;

        if (currentFindRoutine != null)
        {
            StopCoroutine(currentFindRoutine);
        }

        currentFindRoutine = StartCoroutine(InteractWhenReady(interactObj));
    }

    public void SetNavAgentActive(bool state)
    {
        if (!state)
        {
            NavAgent.ResetPath();
        }

        NavAgent.enabled = state;
    }

    IEnumerator InteractWhenReady(INpcInteractable interactObj)
    {
        thisNpc.CurrentInteractStatus = InteractStatus.FindingPath;
        yield return new WaitForSeconds(2);

        NavAgent.SetDestination(interactObj.GetLocation());

        yield return new WaitUntil(() => NavAgent.hasPath);

        thisNpc.CurrentInteractStatus = InteractStatus.WaitingUntilInRange;
        yield return new WaitUntil(() => NavAgent.remainingDistance < 1); // this needs to be replaced, race condition when navagent resets

        thisNpc.CurrentInteractStatus = InteractStatus.Interacting;
        yield return currentInteractRoutine = StartCoroutine(interactObj.Interact(thisNpc));

        thisNpc.OnInteractComplete();
        currentTargetInteractObj = null;
        thisNpc.CurrentInteractStatus = InteractStatus.Idle;
    }

    public void GiveTemporaryPriorityBoost()
    {
        StartCoroutine(IncreasedPriorityRoutine());
    }

    IEnumerator IncreasedPriorityRoutine()
    {
        int oldPriority = NavAgent.avoidancePriority;
        NavAgent.avoidancePriority += 100;

        yield return new WaitForSeconds(3);

        NavAgent.avoidancePriority = oldPriority;
    }

    private void StopAllInteractRoutines()
    {
        if (currentFindRoutine != null)
            StopCoroutine(currentFindRoutine);

        if (currentInteractRoutine != null)
            StopCoroutine(currentInteractRoutine);
    }
}

public enum InteractStatus
{
    Idle,
    FindingPath,
    WaitingUntilInRange,
    Interacting,
}
