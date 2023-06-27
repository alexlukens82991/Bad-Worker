using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInterface : MonoBehaviour
{
    [Header("Bool Names")]
    [SerializeField] private string isSus;

    [Header("Trigger Names")]
    [SerializeField] private string sitDown;
    [SerializeField] private string standUp;

    [Header("Cache")]
    [SerializeField] private Animator animator;
}
