using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIconManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float iconDisplayLength;

    [Header("Cache")]
    [SerializeField] private IconDictionary iconDictionary;
    [SerializeField] private Image iconImage;
    [SerializeField] private CanvasGroup iconCg;

    private Coroutine currentFlashRoutine;

    public void FlashIcon(string name)
    {
        Sprite foundSprite = iconDictionary.GetIcon(name);

        if (foundSprite == null)
        {
            Debug.LogError("Error finding sprite in icon dictionary: " + name);
        }

        if (currentFlashRoutine != null)
            StopCoroutine(currentFlashRoutine);

        currentFlashRoutine = StartCoroutine(FlashIconRoutine(foundSprite));
        
    }

    IEnumerator FlashIconRoutine(Sprite sprite)
    {
        iconImage.sprite = sprite;
        SetCanvasGroupVisable(iconCg, true);

        yield return new WaitForSeconds(iconDisplayLength);

        SetCanvasGroupVisable(iconCg, false);
        iconImage.sprite = null;

    }

    public void SetCanvasGroupVisable(CanvasGroup cg, bool active)
    {
        cg.alpha = active ? 1 : 0;
        cg.interactable = active;
        cg.blocksRaycasts = active;
    }
}
