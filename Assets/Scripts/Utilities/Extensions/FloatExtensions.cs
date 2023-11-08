using UnityEngine;

public static class FloatExtensions
{
    public static float Round(this float input, float multiple = 1) =>
        Mathf.Round(input / multiple) * multiple;
}
