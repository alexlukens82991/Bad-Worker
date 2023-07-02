using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Icon Dictionary", menuName = "Icons/Icon Dictionary")]
public class IconDictionary : ScriptableObject
{
    public Sprite[] Icons;

    public Sprite GetIcon(string iconName)
    {
        foreach (Sprite icon in Icons)
        {
            if (icon.name.Equals(iconName))
                return icon;
        }

        return null;
    }
}
