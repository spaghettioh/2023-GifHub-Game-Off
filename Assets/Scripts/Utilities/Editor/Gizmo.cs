using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Gizmo : MonoBehaviour
{
    private enum Shape
    {
        Cube,
        Sphere,
        WireCube,
        WireSphere,
    }

    [SerializeField]
    private Shape _shape;

    [SerializeField]
    private Color _color = Color.green;

    [SerializeField]
    private float _size;

    [SerializeField]
    private Vector3 _offset;

    [FormerlySerializedAs("_useCollider")]
    [SerializeField]
    private bool _useSphereCollider;

    [SerializeField]
    private SphereCollider _sphereCollider;

    [SerializeField]
    private bool _selectedOnly;

    private void OnDrawGizmos()
    {
        if (_selectedOnly)
        {
            return;
        }
        BuildAndDrawGizmo();
    }

    private void OnDrawGizmosSelected()
    {
        if (!_selectedOnly)
        {
            return;
        }
        BuildAndDrawGizmo();
    }

    private void BuildAndDrawGizmo()
    {
        Gizmos.color = _color;
        var size = _size;
        var position = transform.position + _offset;
        if (_useSphereCollider)
        {
            size = _sphereCollider.radius;
            position += _sphereCollider.center;
        }
        DrawShape(position, size);
    }

    private void DrawShape(Vector3 position, float size)
    {
#if UNITY_EDITOR
        var selected = Array.IndexOf(Selection.objects, gameObject) > -1;
        if (_selectedOnly && !selected)
        {
            return;
        }

        switch (_shape)
        {
            case Shape.Cube:
                Gizmos.DrawCube(position, Vector3.one * size);
                break;
            case Shape.Sphere:
                Gizmos.DrawSphere(position, size);
                break;
            case Shape.WireCube:
                Gizmos.DrawWireCube(position, Vector3.one * size);
                break;
            case Shape.WireSphere:
                Gizmos.DrawWireSphere(position, size);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
#endif
    }

    private void OnValidate()
    {
        if (!_useSphereCollider)
        {
            _sphereCollider = null;
            return;
        }

        _size = 0;
        if (TryGetComponent(out _sphereCollider))
        {
            return;
        }

        Debug.LogError("Must reference a collider!");
    }
}
