using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSOSetter : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private TransformAnchorSO _anchor;

    private void Awake()
    {
        if (_target != null)
        {
            _anchor.SetTransform(_target);
            return;
        }
        _anchor.SetTransform(transform);
    }
}
