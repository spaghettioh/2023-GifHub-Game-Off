using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Data_UI_SubstringColorMap",
    menuName = "Data/UI/Substring color map"
)]
public class SubstringColorMapSO : ScriptableObject
{
    [field: SerializeField]
    public List<ColorMapRow> Map { get; private set; }

    public Color GetColor(string tag)
    {
        var color = Color.white;
        var row = Map.Find(row => row.Tag == tag);
        if (row != null)
        {
            color = row.ColorSO.RGBA;
        }
        return color;
    }
}
