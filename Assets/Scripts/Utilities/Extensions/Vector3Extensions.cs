using UnityEngine;

public static class Vector3Extensions
{
    // By Tarodev: https://youtu.be/8ZxVBCvJDWk?t=531
    private static Matrix4x4 _isometricMatrix =
        Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIsometric(this Vector3 input) =>
        _isometricMatrix.MultiplyPoint3x4(input);
}
