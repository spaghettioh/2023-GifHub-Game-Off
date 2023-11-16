using UnityEngine;

public class ClumpTelemetrySystem : MonoBehaviour
{
    [SerializeField]
    private ClumpRuntimeDataSO _clumpRuntimeData;

    private void FixedUpdate()
    {
        if (_clumpRuntimeData.Transform.IsNull())
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(
            _clumpRuntimeData.Body.velocity.normalized
        );
    }
}
