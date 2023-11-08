using System;
using UnityEngine;

[Serializable]
public class LineOfSightProcedure
{
    [SerializeField]
    private LayerMask _layerMask;

    private Transform _t;
    private Vector3 _Position => _t.position;

    public void Initialize(Transform t) => _t = t;

    public bool Try(Collider target, float range, out RaycastHit hit)
    {
        var direction = (target.transform.position - _Position).normalized;
        Physics.Raycast(_Position, direction, out hit, range, _layerMask);
        return hit.collider == target;
    }
}
