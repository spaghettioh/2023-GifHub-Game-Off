using System;
using UnityEngine;

public class DetectionRangeSystem : MonoBehaviour
{
    public event Action<Collider> OnEnterRange;
    public event Action<Collider> OnExitRange;

    [SerializeField]
    private LayerMask _targetLayers;

    private SphereCollider _detectionRange;
    public float Range => _detectionRange.radius;

    private void Awake() => TryGetComponent(out _detectionRange);

    private void OnTriggerEnter(Collider other) =>
        other.RaiseIfLayerMatch(_targetLayers, OnEnterRange);

    private void OnTriggerExit(Collider other) =>
        other.RaiseIfLayerMatch(_targetLayers, OnExitRange);
}
