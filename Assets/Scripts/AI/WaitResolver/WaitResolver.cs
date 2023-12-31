using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class WaitResolver : MonoSingleton<WaitResolver>
{
    public List<WaitTicket> WaitTickets;

    private void Start()
    {
        StartCoroutine(ResolveConflictsRoutine());
    }

    IEnumerator ResolveConflictsRoutine()
    {
        do
        {
            ResolveConflicts();

            yield return new WaitForSeconds(2);
        } while (true);
    }

    private void ResolveConflicts()
    {
        if (WaitTickets.Count == 0)
            return;

        foreach (WaitTicket waitTicket in WaitTickets)
        {
            if (waitTicket.IsWaitingOn.Count == 0)
            {
                CloseTicket(waitTicket);
                continue;
            }


            foreach (NPC npcWaitingOn in waitTicket.IsWaitingOn)
            {
                WaitTicket foundWaitTicket = GetWaitTicket(npcWaitingOn);

                if (foundWaitTicket == null)
                    continue;

                foreach (NPC npc in foundWaitTicket.IsWaitingOn)
                {
                    // this NPC is waiting on the original NPC too
                    if (npc == waitTicket.Requester)
                    {
                        SettleConflict(waitTicket, foundWaitTicket);
                    }
                }
            }
        }

    }

    private void SettleConflict(WaitTicket npc1, WaitTicket npc2)
    {
        Debug.LogWarning("Canadian Stand Off detected! Resolving...");
        WaitTicket alpha;

        if (npc1.TicketIsDone || npc2.TicketIsDone)
        {
            //if (npc1.TicketIsDone)
            //    alpha = npc1;
            //else if (npc2.TicketIsDone)
            //    alpha = npc2;
            //else
            //{
            //    Debug.LogError("Something weird happened while settling conflict..");
            //    alpha = npc1;
            //}
            return;
        }
        else
        {
            if (Random.value > 0.5f)
                alpha = npc1;
            else
                alpha = npc2;
        }

        alpha.Requester.NpcController.GiveTemporaryPriorityBoost();
        CloseTicket(alpha);
    }

    public void RequestWaitTicket(NPC requestingNpc, List<NPC> isWaitingOn)
    {
        WaitTicket foundTicket = GetWaitTicket(requestingNpc);

        if (foundTicket != null)
        {
            foundTicket.IsWaitingOn = foundTicket.IsWaitingOn.Union(isWaitingOn).ToList();
        }
        else
        {
            CreateNewWaitTicket(requestingNpc, isWaitingOn);
        }
    }

    public void CancelWaitTicket(NPC requestingNpc)
    {
        CloseTicket(GetWaitTicket(requestingNpc));
    }

    private void CloseTicket(WaitTicket waitTicket)
    {
        waitTicket.Requester.SetWaitState(false);
        waitTicket.TicketIsDone = true;
        CleanUpTickets();
    }

    private void CleanUpTickets()
    {
        List<WaitTicket> cleanedList = new();

        foreach (WaitTicket ticket in WaitTickets)
        {
            if (!ticket.TicketIsDone)
            {
                cleanedList.Add(ticket);
            }
        }

        WaitTickets = cleanedList;
    }

    public void UpdateIsWaitingOn(NPC requestingNpc, List<NPC> isWaitingOn)
    {
        WaitTicket foundTicket = GetWaitTicket(requestingNpc);

        if (foundTicket == null)
        {
            RequestWaitTicket(requestingNpc, isWaitingOn);
            return;
        }

        foundTicket.IsWaitingOn = isWaitingOn;
    }

    private void CreateNewWaitTicket(NPC requestingNpc, List<NPC> isWaitingOn)
    {
        WaitTicket newTicket = new(requestingNpc, isWaitingOn);

        WaitTickets.Add(newTicket);
    }

    private WaitTicket GetWaitTicket(NPC requester)
    {
        foreach (WaitTicket waitTicket in WaitTickets)
        {
            if (waitTicket.Requester == requester)
                return waitTicket;
        }

        return null;
    }
}

[Serializable]
public class WaitTicket
{
    public NPC Requester;
    public List<NPC> IsWaitingOn;
    public bool TicketIsDone;
    public WaitTicket(NPC requestingNpc, List<NPC> isWaitingOn)
    {
        Requester = requestingNpc;
        IsWaitingOn = isWaitingOn;
    }
}
