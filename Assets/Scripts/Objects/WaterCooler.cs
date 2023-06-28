using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCooler : MonoBehaviour, INpcInteractable
{
    [SerializeField] private float amountPerDrink;
    [SerializeField] private string ID;
    public string GetID()
    {
        return ID;
    }

    public Vector3 GetLocation()
    {
        return transform.position;
    }

    public void Interact(NPC interactingNpc)
    {
        print("Getting water!");
        interactingNpc.Vitals.AddToVital(VitalType.Hydration, amountPerDrink);
    }
}