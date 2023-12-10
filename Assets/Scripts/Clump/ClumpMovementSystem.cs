using UnityEngine;

public class ClumpMovementSystem : MonoBehaviour
{
    [SerializeField]
    private ClumpRuntimeDataSO _clumpData;

    [SerializeField]
    private TransformAnchorSO _inputTelemetryAnchor;

    private Vector3 _directionalInput;
    private bool _isInputActive;
    private Transform _t;
    private Rigidbody _b;
    private SphereCollider _c;
    private float _force;
    private float _maxSpeed;

    private void Awake()
    {
        TryGetComponent(out _t);
        TryGetComponent(out _c);
        TryGetComponent(out _b);
        _clumpData.Initialize(_t, _b, _c);
        _c.radius = _clumpData.MinColliderRadius;
        _force = _clumpData.MinMoveForce;
        _maxSpeed = _clumpData.MaxSpeed;
    }

    private void OnEnable() =>
        InputManager.OnDirectionalInput += HandleDirectionalInput;

    private void OnDisable() =>
        InputManager.OnDirectionalInput -= HandleDirectionalInput;

    private void FixedUpdate() => ApplyTorqueOnTelemetryAxis();

    private void HandleDirectionalInput(Vector2 input) =>
        _isInputActive = input != Vector2.zero;

    private void ApplyTorqueOnTelemetryAxis()
    {
        if (!_isInputActive || _b.velocity.magnitude > _maxSpeed)
        {
            return;
        }

        var fdt = Time.fixedDeltaTime;
        _b.AddTorque(
            _force * fdt * _inputTelemetryAnchor.Transform.right,
            ForceMode.Force
        );
    }
}
