using UnityEngine;

public class ClumpTelemetrySystem : MonoBehaviour
{
    [SerializeField]
    private ClumpRuntimeDataSO _clumpRuntimeData;

    private Vector3 _previousVelocity;

    private void Start() =>
        transform.rotation = Quaternion.Euler(Vector3.forward);

    private void Update()
    {
        if (_clumpRuntimeData.Transform.IsNull())
        {
            return;
        }

        var newVelocity = _clumpRuntimeData.Body.velocity;
        if (newVelocity == Vector3.zero)
        {
            return;
        }

        var lerp = Vector3.Lerp(
            _previousVelocity, newVelocity, 360 * Time.deltaTime
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation, Quaternion.LookRotation(lerp),
            360 * Time.deltaTime
        );

        _previousVelocity = _clumpRuntimeData.Body.velocity;

        // transform.rotation =
        //     Quaternion.LookRotation(_clumpRuntimeData.Body.velocity);
        //
        // transform.rotation = Quaternion.RotateTowards(
        //     transform.rotation,
        //     Quaternion.Euler(_clumpRuntimeData.Body.velocity), 360
        // );
    }
}
