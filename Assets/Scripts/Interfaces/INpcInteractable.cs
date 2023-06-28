using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INpcInteractable
{
    public string GetID();
    public Vector3 GetLocation();
    public void Interact(NPC interactingNpc);

}
