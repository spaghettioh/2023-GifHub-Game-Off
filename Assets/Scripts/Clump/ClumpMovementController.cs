using UnityEngine;

public class ClumpMovementController : MonoBehaviour
{
    private enum ForceType
    {
        Force,
        RelativeForce,
        RelativeTorque,
        Torque,
    }

    // Take directional input
    // Calculate the force direction based on input
    // Apply force in that direction
    [SerializeField]
    private SphereCollider _collider;

    [SerializeField]
    private Rigidbody _body;

    [SerializeField]
    private Transform _telemetry;

    [SerializeField]
    private bool _showTelemetry;

    [SerializeField]
    private ForceType _forceType;

    [SerializeField]
    private float _force;

    [SerializeField]
    private ForceMode _forceMode;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private ClumpRuntimeDataSO _clumpData;

    private Vector3 _directionalInput;
    private Transform _t;

    private void Awake()
    {
        _telemetry.SetParent(null);
        TryGetComponent(out _t);
        _t.position = new(0, _clumpData.MinColliderRadius, 0);

        TryGetComponent(out _collider);
        TryGetComponent(out _body);
        _clumpData.ConfigureData(transform, _collider);
        ConfigureController();
    }

    private void OnEnable()
    {
        InputManager.OnDirectionalInput += HandleDirectionalInput;
    }

    private void OnDisable()
    {
        InputManager.OnDirectionalInput -= HandleDirectionalInput;
    }

    private void Update()
    {
        _clumpData.SetVelocity(_body.velocity.magnitude);
        ShowTelemetry(_showTelemetry);
        CalculateForceDirection();
    }

    private void FixedUpdate()
    {
        ApplyDirectionalForce();
    }

    private void HandleDirectionalInput(Vector2 input)
    {
        _directionalInput = new(input.x, 0, input.y);
    }

    private void ConfigureController()
    {
        _collider.radius = _clumpData.MinColliderRadius;
        _force = _clumpData.MinMoveForce;
        _maxSpeed = _clumpData.MaxSpeed;
    }

    private void CalculateForceDirection()
    {
        if (_directionalInput == Vector3.zero)
        {
            return;
        }
        var position = _telemetry.position;
        var relative = position + _directionalInput.ToIsometric() - position;
        var rot = Quaternion.LookRotation(relative, Vector3.up);

        _telemetry.rotation = Quaternion.RotateTowards(
            _telemetry.rotation, rot, 360
        );
    }

    private void ApplyDirectionalForce()
    {
        if (_directionalInput == Vector3.zero
            || _body.velocity.magnitude > _maxSpeed)
        {
            return;
        }

        var fdt = Time.fixedDeltaTime;

        switch (_forceType)
        {
            case ForceType.Force:
                _body.AddForce(_force * fdt * _telemetry.forward, _forceMode);
                break;

            case ForceType.RelativeForce:
                _body.AddRelativeForce(
                    _force * fdt * _telemetry.forward, _forceMode
                );
                break;

            case ForceType.RelativeTorque:
                _body.AddRelativeTorque(
                    _force * fdt * _telemetry.right, _forceMode
                );
                break;

            case ForceType.Torque:
                _body.AddTorque(_force * fdt * _telemetry.right, _forceMode);
                break;

            default:
                throw new("Unmapped ForceType");
        }
    }

    private void ShowTelemetry(bool show)
    {
        _telemetry.gameObject.SetActive(show);
        //
        // if (show)
        // {
        //     _telemetry.position = transform.position;
        // }
    }
}
