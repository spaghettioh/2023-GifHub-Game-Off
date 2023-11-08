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

    [SerializeField]
    private TransformEventSO _event;

    [SerializeField]
    private TransformAnchorSO _target;

    [SerializeField]
    private CinemachineVirtualCameraBase _cinemachineCamera;

    [SerializeField]
    private Setting _setting;

    private void OnEnable() =>
        _event.OnEventRaised += OnEvent;

    private void OnDisable() =>
        _event.OnEventRaised -= OnEvent;

    private void OnEvent(Transform _)
    {
        if (_setting == Setting.LookAt)
        {
            _cinemachineCamera.LookAt = _target.Transform;
            return;
        }
        _cinemachineCamera.Follow = _target.Transform;
    }
}
