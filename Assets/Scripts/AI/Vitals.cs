using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vitals : MonoBehaviour, ICanBeInitialized
{
    public VitalInfo Hydration;
    public VitalInfo Bladder;
    public VitalInfo Mood;
    public VitalInfo Paranoia;


    private VitalInfo[] vitalsList;

    [Header("Settings")]
    [SerializeField] private float bladderUpdateFreq;
    [SerializeField] private float bladderMaxAdjustment;

    [Header("Cache")]
    [SerializeField] private Animator StateMachine;

    public void Initialize()
    {
        vitalsList = new VitalInfo[] { Hydration, Bladder , Mood, Paranoia};

        StartCoroutine(UpdateVitalsRoutine());
        StartCoroutine(BladderRoutine());

        UpdateVitals();
    }

    public void AddToVital(VitalType vitalType, float amt)
    {
        VitalInfo vitalToChange = GetVital(vitalType);

        vitalToChange.Value += amt;

        if (vitalToChange.Value > 100)
            vitalToChange.Value = 100;
        else if (vitalToChange.Value < 0)
            vitalToChange.Value = 0;

        UpdateVitals();
    }

    private void UpdateVitals()
    {
        StateMachine.SetFloat("Hydration", Hydration.Value);
        StateMachine.SetFloat("Bladder", Bladder.Value);
        StateMachine.SetFloat("Mood", Mood.Value);
        StateMachine.SetFloat("Paranoia", Paranoia.Value);
    }

    IEnumerator UpdateVitalsRoutine()
    {
        do
        {
            UpdateVitals();
            yield return new WaitForSeconds(1);
        } while (true);
    }

    IEnumerator BladderRoutine()
    {
        VitalInfo hydrationInfo = GetVital(VitalType.Hydration);
        VitalInfo bladderInfo = GetVital(VitalType.Bladder);
        do
        {
            if (hydrationInfo.Value > 0)
            {
                float randomAmt = Random.Range(0f, bladderMaxAdjustment);

                if (hydrationInfo.Value < randomAmt)
                    randomAmt = hydrationInfo.Value;

                hydrationInfo.Value -= randomAmt;
                bladderInfo.Value += randomAmt / 2;
            }

            yield return new WaitForSeconds(Random.Range(0f, bladderUpdateFreq));
        } while (Application.isPlaying);
    }

    private VitalInfo GetVital(VitalType type)
    {
        foreach (VitalInfo vital in vitalsList)
        {
            if (vital.VitalType == type)
                return vital;
        }

        Debug.LogError("COULD NOT FIND VITAL: " + type);
        return null;
    }


}

[Serializable]
public class VitalInfo
{
    public VitalType VitalType;
    public float Value;
}

public enum VitalType
{
    Hydration,
    Paranoia,
    Bladder, 
    Mood
}
