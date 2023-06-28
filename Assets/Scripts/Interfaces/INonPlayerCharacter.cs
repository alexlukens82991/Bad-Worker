using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INonPlayerCharacter
{
    public string GetID();
    public void Interact();
    public void PickPocket();

}
