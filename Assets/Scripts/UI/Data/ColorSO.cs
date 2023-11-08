using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Color_NAME", menuName = "Data/Color data")]
public class ColorSO : ScriptableObject
{
    [field: SerializeField]
    public Color Color { get; private set; }
}