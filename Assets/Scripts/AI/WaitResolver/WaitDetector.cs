using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitDetector : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private NPC thisNpc;

    private List<NPC> isWaitingOn = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            NPC foundNpc = other.GetComponentInParent<NPC>();

            if (foundNpc == null)
                Debug.LogError("returned null NPC even though Obj tagged as NPC");

            if (foundNpc.GetIsAtWorkStation())
                return;

            if (!isWaitingOn.Contains(foundNpc))
            {
                AddIsWaitingOn(foundNpc);
            }

            thisNpc.SetWaitState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            NPC foundNpc = other.GetComponentInParent<NPC>();

            if (foundNpc == null)
                Debug.LogError("returned null NPC even though Obj tagged as NPC");

            if (isWaitingOn.Contains(foundNpc))
            {
                RemoveIsWaitingOn(foundNpc);
            }
        }
    }

    private void AddIsWaitingOn(NPC foundNpc)
    {
        isWaitingOn.Add(foundNpc);
        WaitResolver.Instance.UpdateIsWaitingOn(thisNpc, isWaitingOn);
    }

    private void RemoveIsWaitingOn(NPC foundNpc)
    {
        isWaitingOn.Remove(foundNpc);

        if (isWaitingOn.Count > 0)
            WaitResolver.Instance.UpdateIsWaitingOn(thisNpc, isWaitingOn);
        else
            WaitResolver.Instance.CancelWaitTicket(thisNpc);
    }
}
