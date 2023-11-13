using System;
using UnityEngine;

[Serializable]
public class MiniMapCameraHandler
{
    [SerializeField]
    private Transform _camera;

    // FR I want to adjust settings for different map rotations
    internal void UpdateCameraPosition(Vector3 position)
    {
        var x = position.x;
        var z = position.z;
        _camera.position = new(x, _camera.position.y, z);
    }
}
