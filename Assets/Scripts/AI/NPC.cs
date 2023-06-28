using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, INonPlayerCharacter
{
    [SerializeField] private string ID;

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
