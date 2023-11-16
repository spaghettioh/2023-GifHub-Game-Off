using UnityEngine;

public class TelemetrySystem : MonoBehaviour
{
    [SerializeField]
    private Transform _t;
    private Vector3 _Pos => _t.position;

    [SerializeField]
    private InputEventSO _inputEvent;

    [SerializeField]
    private Vector3 _movementInput;

    private void Awake() => _t = transform;

    private void OnEnable() =>
        InputManager.OnDirectionalInput += SetDirectionalInput;

    private void OnDisable() =>
        InputManager.OnDirectionalInput -= SetDirectionalInput;

    public void Update()
    {
        if (_movementInput == Vector3.zero)
        {
            return;
        }

        var matrix = Matrix4x4.Rotate(Camera.main.transform.rotation);
        var skewedInput = matrix.MultiplyPoint3x4(_movementInput);

        var relative = _Pos + skewedInput - _Pos;
        var rot = Quaternion.LookRotation(relative, Vector3.up);

        _t.rotation = Quaternion.RotateTowards(
            _t.rotation, rot, 360 * Time.deltaTime
        );
    }

    private void SetDirectionalInput(Vector2 input) =>
        _movementInput = new(input.x, 0, input.y);
}
