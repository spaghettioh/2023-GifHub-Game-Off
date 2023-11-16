using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ClumpMovementController : MonoBehaviour
{
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
    private float _force;

    [SerializeField]
    private ForceMode _forceMode;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private ClumpRuntimeDataSO _clumpData;

    [FormerlySerializedAs("_telemetryAnchor")]
    [SerializeField]
    private TransformAnchorSO _inputTelemetryAnchor;

    private Vector3 _movementInput;
    private Transform _t;

    [SerializeField]
    private TMP_Text _uiText;

    private void Awake()
    {
        // _telemetry.SetParent(null);
        TryGetComponent(out _t);
        _t.position = new(0, _clumpData.MinColliderRadius, 0);

        TryGetComponent(out _collider);
        TryGetComponent(out _body);
        _clumpData.ConfigureData(_t, _body, _collider);
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

    private void Start()
    {
        _telemetry = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        ApplyTorqueOnTelemetryAxis();
    }

    private void HandleDirectionalInput(Vector2 input)
    {
        _movementInput = input;
    }

    private void ConfigureController()
    {
        _collider.radius = _clumpData.MinColliderRadius;
        _force = _clumpData.MinMoveForce;
        _maxSpeed = _clumpData.MaxSpeed;
    }

    private void ApplyTorqueOnTelemetryAxis()
    {
        if (_movementInput == Vector3.zero
            || _body.velocity.magnitude > _maxSpeed)
        {
            return;
        }

        var fdt = Time.fixedDeltaTime;
        _body.AddTorque(
            _force * fdt * _inputTelemetryAnchor.Transform.right,
            ForceMode.Force
        );
    }
}
