using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkStation : MonoBehaviour, INpcInteractable
{
    [Header("Status")]
    public bool NpcIsAtWorkstation;

    [Header("Cache")]
    [SerializeField] private Transform chair;
    [SerializeField] private Vector3 chairInPosition;
    [SerializeField] private Vector3 amountToMoveChair;
    [SerializeField] private Vector3 playerDistanceToChair;

    private void Start()
    {
        chairInPosition = chair.position;
    }

    public string GetID()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetLocation()
    {
        return chair.position;
    }

    public IEnumerator GetUpFromWorkStation(NPC requestingNpc)
    {
        //move chair out.
        chair.position += amountToMoveChair;
        requestingNpc.transform.position += amountToMoveChair;

        //position player
        requestingNpc.AnimationInterface.StandUpIfSitting();

        // wait for sit to complete.
        yield return new WaitForSeconds(3);

        //set final positions of player and chair.
        requestingNpc.NpcController.SetNavAgentActive(true);
        NavMesh.SamplePosition(requestingNpc.transform.position, out NavMeshHit hit, 2, requestingNpc.NpcController.NavAgent.areaMask);
        requestingNpc.transform.position = hit.position;

        chair.position = chairInPosition;
        NpcIsAtWorkstation = false;
    }

    public IEnumerator Interact(NPC interactingNpc)
    {
        NpcIsAtWorkstation = true;
        //turn off navagent
        interactingNpc.NpcController.SetNavAgentActive(false);

        //move chair out.
        chair.position += amountToMoveChair;

        //position player to sit
        interactingNpc.transform.position = chair.position + playerDistanceToChair;
        interactingNpc.transform.rotation = chair.rotation;

        //sit player
        interactingNpc.AnimationInterface.SitDown();

        // wait for sit to complete.
        yield return new WaitForSeconds(3);

        //set final positions of player and chair.
        chair.position -= amountToMoveChair;
        interactingNpc.transform.position -= amountToMoveChair;
    }
}
