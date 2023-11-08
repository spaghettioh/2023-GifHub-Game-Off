using System;
using UnityEngine;

public static class DetectionRangeExtensions
{
    public static void RaiseIfLayerMatch(
        this Collider collider, LayerMask layerMask, Action<Collider> action
    )
    {
        if (layerMask.Contains(collider.gameObject.layer))
        {
            action?.Invoke(collider);
        }
    }
}
