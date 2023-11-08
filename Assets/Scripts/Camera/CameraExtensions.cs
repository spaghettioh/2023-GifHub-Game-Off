using UnityEngine;

internal static class CameraExtensions
{
    /// <summary>
    ///     x: braking, y: default, z: boosting
    /// </summary>
    private static Vector3 _velocities;

    internal static void SetVelocities(Vector3 velocities)
    {
        _velocities = velocities;
    }

    internal static float GetVelocityRemap(
        this float input, Vector3 outputRange, out float progress
    )
    {
        var isBelowDefault = input <= _velocities.y;
        var minInput = isBelowDefault ? _velocities.x : _velocities.y;
        var maxInput = isBelowDefault ? _velocities.y : _velocities.z;
        var minOutput = isBelowDefault ? outputRange.x : outputRange.y;
        var maxOutput = isBelowDefault ? outputRange.y : outputRange.z;
        var target = input.Remap(minInput, maxInput, minOutput, maxOutput);
        progress = Mathf.InverseLerp(minInput, maxInput, input);
        return target;
    }

    internal static float GetVelocityRemap(
        this float input, Vector3 outputRange
    ) =>
        input.GetVelocityRemap(outputRange, out _);
}
