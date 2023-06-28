using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletManager : MonoSingleton<ToiletManager>
{
    [SerializeField] private List<Toilet> Toilets;

    public Toilet GetClosest(NPC requestingNpc, Toilet lastVisited)
    {
        Toilet closest = null;
        float closestDistance = float.MaxValue;

        foreach (Toilet cooler in Toilets)
        {
            if (cooler == null)
                continue;

            if (cooler == lastVisited)
                continue;

            float dist = requestingNpc.GetDistanceTo(cooler.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = cooler;
            }
        }

        print("found toilet: " + closest.gameObject.name);

        return closest;
    }
}
