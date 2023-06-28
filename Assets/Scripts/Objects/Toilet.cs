using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour, INpcInteractable
{
    [SerializeField] private string ID;

    public string GetID()
    {
        return ID;
    }

    public Vector3 GetLocation()
    {
        return transform.position;
    }

    public IEnumerator Interact(NPC interactingNpc)
    {
        yield return new WaitForSeconds(Random.Range(5, 15f));

        interactingNpc.Vitals.AddToVital(VitalType.Bladder, -101);
        Debug.Log("Used bathroom!");
    }
}
