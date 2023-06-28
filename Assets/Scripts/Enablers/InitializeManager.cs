using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitializeManager : MonoSingleton<InitializeManager>
{
    private void Start()
    {
        var canBeInitializeds = FindObjectsOfType<MonoBehaviour>().OfType<ICanBeInitialized>();

        foreach (MonoBehaviour item in canBeInitializeds)
        {
            ICanBeInitialized i = (ICanBeInitialized)item;
            print("initializing: " + item.GetType());
            i.Initialize();
        }
    }
}
