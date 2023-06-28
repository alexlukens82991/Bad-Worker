using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCoolerManager : MonoSingleton<WaterCoolerManager>
{
    [SerializeField] private List<WaterCooler> WaterCoolers;

    public WaterCooler GetClosestWaterCooler(NPC requestingNpc, WaterCooler lastVisitedCooler)
    {
        WaterCooler closestCooler = null;
        float closestDistance = float.MaxValue;

        foreach (WaterCooler cooler in WaterCoolers)
        {
            if (cooler == null)
                continue;

            if (cooler == lastVisitedCooler)
                continue;

            float dist = requestingNpc.GetDistanceTo(cooler.transform.position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestCooler = cooler;
            }
        }

        print("found cooler: " + closestCooler.gameObject.name);

        return closestCooler;
    }
}
