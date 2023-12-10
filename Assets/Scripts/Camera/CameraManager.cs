using System;
using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private enum Angle
    {
        Default,
        High,
        Low,
    }

    [Serializable]
    private class CameraSetup
    {
        public float XAngle, Distance, LookAhead, LookAheadSmooth;
    }

    [SerializeField]
    private CameraSetup _default;

    [SerializeField]
    private CameraSetup _high;

    [SerializeField]
    private CameraSetup _low;

    [SerializeField]
    private Transform _virtualCamera;
    private CinemachineFramingTransposer _framingTransposer;

    [Header("Audio")]
    [SerializeField]
    private SoundEffectAudioDataSO _moveLeftSound;

    [SerializeField]
    private SoundEffectAudioDataSO _moveRightSound;

    [Header("Listening for...")]
    [SerializeField]
    private BoolEventSO _moveCameraEvent;

    [Header("Broadcasting to...")]
    [SerializeField]
    private AudioEventSO _audioEvent;

    private CameraSetup _currentSetup;
    private bool _canMove = true;

    private void OnValidate()
    {
        SetFields();
        _virtualCamera.rotation = Quaternion.Euler(
            new(
                _currentSetup.XAngle, _virtualCamera.transform.eulerAngles.y, 0f
            )
        );
        _framingTransposer.m_CameraDistance = _currentSetup.Distance;
        _framingTransposer.m_LookaheadTime = _currentSetup.LookAhead;
        _framingTransposer.m_LookaheadSmoothing = _currentSetup.LookAheadSmooth;
    }

    private void SetFields()
    {
        _currentSetup = _default;
        _framingTransposer = _virtualCamera
            .GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Awake()
    {
        _currentSetup = _default;
        SetFields();
    }

    private void OnEnable() =>
        _moveCameraEvent.OnEventRaised += HandleMoveCamera;

    private void OnDisable() =>
        _moveCameraEvent.OnEventRaised -= HandleMoveCamera;

    private void Start()
    {
        HandleMoveCamera(true);
    }

    private void HandleMoveCamera(bool isLeft)
    {
        if (!_canMove)
        {
            return;
        }

        var sfx = isLeft ? _moveLeftSound : _moveRightSound;
        _audioEvent.RaisePlayback(sfx, name);
        StartCoroutine(RotateViewRoutine(isLeft));
    }

    private IEnumerator RotateViewRoutine(bool isLeft)
    {
        _canMove = false;
        var camera = _framingTransposer.transform;
        var euler = camera.eulerAngles;
        var direction = isLeft ? -90f : 90f;
        camera.DORotate(new(euler.x, euler.y + direction, 0f), 1f).OnComplete(
            () => _canMove = true
        );
        var previousCamera = _currentSetup;

        // Wait a few frames before checking for blend activity
        yield return null;
        // yield return null;
        // while (_brain.IsBlending)
        // {
        //     yield return null;
        // }
        // _canMove = true;
        // previousCamera.gameObject.SetActive(false);
    }
}
