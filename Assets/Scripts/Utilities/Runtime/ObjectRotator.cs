using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    private enum Axis
    {
        X,
        Y,
        Z,
        XY,
        XZ,
        YZ,
        XYZ,
    }

    [SerializeField]
    private Transform _targetTransform;

    [SerializeField]
    private float _turnSpeed;

    [SerializeField]
    private Axis _axis = Axis.Y;

    private Transform _t;
    private Vector3 _currentRotation, _x, _y, _z;

    private void Awake()
    {
        _x = Vector3.right;
        _y = Vector3.up;
        _z = Vector3.forward;
        _t = _targetTransform.IsNull() ? transform : _targetTransform;
    }

    private void Update()
    {
        var axis = _axis switch
        {
            Axis.X => _x,
            Axis.Y => _y,
            Axis.Z => _z,
            Axis.XY => _x + _y,
            Axis.XZ => _x + _z,
            Axis.YZ => _y + _z,
            Axis.XYZ => _x + _y + _z,
            _ => Vector3.zero,
        };

        _currentRotation += axis * _turnSpeed * Time.deltaTime;
        _t.rotation = Quaternion.Euler(_currentRotation);
    }
}
