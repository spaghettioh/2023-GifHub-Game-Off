using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollowSetterSystem : MonoBehaviour
{
    private enum Setting
    {
        LookAt,
        Follow,
    }

    [FormerlySerializedAs("_atlasSpawned")]
    [SerializeField]
    private TransformEventSO _transformEvent;

    [SerializeField]
    private TransformAnchorSO _target;

    [SerializeField]
    private CinemachineVirtualCameraBase _cinemachineCamera;

    [SerializeField]
    private Setting _setting;

    private void OnEnable() =>
        _transformEvent.OnEventRaised += HandleTransformEvent;

    private void OnDisable() =>
        _transformEvent.OnEventRaised -= HandleTransformEvent;

    private void HandleTransformEvent(Transform _)
    {
        if (_setting == Setting.LookAt)
        {
            _cinemachineCamera.LookAt = _target.Transform;
            return;
        }
        _cinemachineCamera.Follow = _target.Transform;
    }
}
