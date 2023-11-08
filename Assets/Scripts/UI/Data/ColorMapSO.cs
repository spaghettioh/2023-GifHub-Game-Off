using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Data_UI_DescriptionColorMap",
    menuName = "Data/UI/Description color map"
)]
public class ColorMapSO : ScriptableObject
{
    [field: SerializeField]
    public List<ColorMapRow> Map { get; private set; }

    public Color GetColor(string tag)
    {
        var color = Color.white;
        var row = Map.Find(row => row.Tag == tag);
        if (row != null)
        {
            color = row.ColorSO.Color;
        }
        return color;
    }
}
