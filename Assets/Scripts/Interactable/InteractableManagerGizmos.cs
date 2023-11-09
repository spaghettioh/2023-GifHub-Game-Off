using System;
using UnityEngine;

[RequireComponent(typeof(InteractableManager))]
public class InteractableManagerGizmos : MonoBehaviour
{
    [Serializable]
    private class Gizmo
    {
        [SerializeField]
        internal Color Color;

        [SerializeField]
        internal float Radius;
    }

    [SerializeField]
    private Gizmo _inSight;

    [SerializeField]
    private Gizmo _interactable;

    [SerializeField]
    private Gizmo _closest;
    private InteractableManager _manager;

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || _manager.IsNull())
        {
            return;
        }

        foreach (var item in _manager.SightRange)
        {
            DrawSphereGizmo(item, _inSight.Color, _inSight.Radius);
        }

        foreach (var item in _manager.InteractableRange)
        {
            DrawSphereGizmo(item, _interactable.Color, _interactable.Radius);
        }

        if (!_manager.Closest.IsNull())
        {
            DrawSphereGizmo(_manager.Closest, _closest.Color, _closest.Radius);
        }
    }

    private void DrawSphereGizmo(IInteractable item, Color color, float radius)
    {
        var pos = item.Position;
        Gizmos.color = color;
        Gizmos.DrawSphere(pos, radius);
    }

    private void OnValidate()
    {
        if (!_manager.IsNull())
        {
            return;
        }
        TryGetComponent(out _manager);
    }
}
