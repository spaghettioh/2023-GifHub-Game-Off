using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MapBoundaryProcedure
{
    [Tooltip("Center e.g. Vector3(200,1100,600)")]
    [SerializeField]
    private float _centerY = 1100f;

    [Tooltip("Overall size e.g. Vector3(1600,1800,2000)")]
    [SerializeField]
    private float _height = 1800f;

    [Tooltip("Additional % to pad the edges of the boundary around the stage")]
    [Range(0, 1)]
    [SerializeField]
    private float _padding;

    /// <summary>
    ///     The stored max extend of the boundary
    /// </summary>
    private Vector3 _max;

    /// <summary>
    ///     The stored min extent of the boundary
    /// </summary>
    private Vector3 _min;

    internal void GenerateBoundary(BoxCollider c, IEnumerable<Bounds> rooms)
    {
        _padding += 1f;
        IEnumerable<Bounds> boundsEnumerable = rooms.ToList();
        var first = boundsEnumerable.First();
        c.center = first.center + Vector3.up * _centerY;
        _min = first.min;
        _max = first.max;
        boundsEnumerable.ToList().ForEach(AdjustBoundaries);
        SetColliderDetails(c);
    }

    private void AdjustBoundaries(Bounds bounds)
    {
        var min = bounds.min;
        var max = bounds.max;
        _min.x = min.x < _min.x ? min.x : _min.x;
        _min.z = min.z < _min.z ? min.z : _min.z;
        _max.x = max.x > _max.x ? max.x : _max.x;
        _max.z = max.z > _max.z ? max.z : _max.z;
    }

    private void SetColliderDetails(BoxCollider c)
    {
        var sizeX = _max.x - _min.x;
        var sizeZ = _max.z - _min.z;
        c.size = new(sizeX * _padding, _height, sizeZ * _padding);

        var centerX = _max.x - sizeX / 2;
        var centerZ = _max.z - sizeZ / 2;
        c.center = new(centerX, _centerY, centerZ);
    }
}
