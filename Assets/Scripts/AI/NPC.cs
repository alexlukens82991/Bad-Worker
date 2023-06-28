using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, INonPlayerCharacter
{
    [SerializeField] private string ID;

    [Header("Scripts")]
    public NpcStateMachine StateMachine;
    public NpcController NpcController;
    public Vitals Vitals;

    private WaterCooler lastVisitedCooler;
    private Toilet lastVisitedToilet;
    public void FindWater()
    {
        WaterCooler targetCooler = WaterCoolerManager.Instance.GetClosestWaterCooler(this, lastVisitedCooler);

        NpcController.FindAndInteract(targetCooler);

        lastVisitedCooler = targetCooler;
    }

    public void FindToilet()
    {
        Toilet target = ToiletManager.Instance.GetClosest(this, lastVisitedToilet);

        NpcController.FindAndInteract(target);

        lastVisitedToilet = target;
    }

    public float GetDistanceTo(Vector3 target)
    {
        NavMeshPath path = new();

        if (!NavMesh.SamplePosition(target, out NavMeshHit hit, 50, NpcController.NavAgent.areaMask))
        {
            Debug.LogError("COULD NOT FIND NAV MESH POSITION FOR: " + target);
        }

        if (!NavMesh.CalculatePath(transform.position, hit.position, NpcController.NavAgent.areaMask, path))
        {
            Debug.LogError("FAILED TO CREATE PATH FOR: " + target);
        }

        float distance = 0;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return distance;
    }

    public void OnInteractComplete()
    {
        print("Interaction complete!");
        StateMachine.OnInteractComplete();
    }

    public string GetID()
    {
        return ID;
    }

    public void Interact()
    {
        print("NPC Interacted with! " + gameObject.name);
    }

    public void PickPocket()
    {
        print("Pickpocket attemnpted on: " + gameObject.name);
    }
}
