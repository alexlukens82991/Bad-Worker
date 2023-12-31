using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INpcInteractable
{
    public string GetID();
    public Vector3 GetLocation();
    public IEnumerator Interact(NPC interactingNpc);

}
