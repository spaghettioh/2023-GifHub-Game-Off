using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    menuName = "Runtime/Transform anchor",
    fileName = "Transform_NAME"
)]
public class TransformAnchorSO : ScriptableObject
{
    public event UnityAction<Transform> OnTransformSet;
    internal bool isSet;

    public Transform Transform { get; private set; }

    public void SetTransform(Transform t)
    {
        Transform = t;
        OnTransformSet?.Invoke(Transform);
    }
}
