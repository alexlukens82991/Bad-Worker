using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Singleton Detected: " + gameObject.name);
        }

        Instance = this as T;
    }
}
