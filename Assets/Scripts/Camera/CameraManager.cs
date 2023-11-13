using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineBrain _brain;

    [SerializeField]
    private CinemachineVirtualCamera _lookingNortheast;
    [SerializeField]
    private CinemachineVirtualCamera _lookingSoutheast;
    [SerializeField]
    private CinemachineVirtualCamera _lookingSouthwest;
    [SerializeField]
    private CinemachineVirtualCamera _lookingNorthwest;

    [SerializeField]
    private BoolEventSO _moveCameraEvent;

    [FormerlySerializedAs("_moveClockwise")]
    [SerializeField]
    private SoundEffectAudioDataSO _moveCWSound;

    [FormerlySerializedAs("_moveCounterclockwise")]
    [SerializeField]
    private SoundEffectAudioDataSO _moveCCWSound;

    [Header("Broadcasting to...")]
    [SerializeField]
    private AudioEventSO _audioEvent;

    private CinemachineVirtualCamera _currentCamera;
    private List<CinemachineVirtualCamera> _cameras = new();
    private int _currentCameraIndex;
    private bool _canMove = true;

    private void Awake()
    {
        _cameras = new()
        {
            _lookingNortheast,
            _lookingSoutheast,
            _lookingSouthwest,
            _lookingNorthwest,
        };
        _currentCamera = _cameras[_currentCameraIndex];
    }

    private void OnEnable()
    {
        _moveCameraEvent.OnEventRaised += HandleMoveCamera;
    }

    private void OnDisable()
    {
        _moveCameraEvent.OnEventRaised -= HandleMoveCamera;
    }
    private void HandleMoveCamera(bool clockwise)
    {
        if (!_canMove)
        {
            return;
        }

        _currentCameraIndex += clockwise ? 1 : -1;

        // TODO OMFGOMASDMOASD THIS SUUUUUUUUCKS
        _currentCameraIndex =
            clockwise ? _currentCameraIndex++ : _currentCameraIndex--;
        if (_currentCameraIndex == -1)
        {
            _currentCameraIndex = _cameras.Count - 1;
        }
        if (_currentCameraIndex >= _cameras.Count)
        {
            _currentCameraIndex = 0;
        }
        print(_currentCameraIndex);

        StartCoroutine(BlendCamerasRoutine());
        var sfx = clockwise ? _moveCWSound : _moveCCWSound;
        _audioEvent.RaisePlayback(sfx, name);
    }

    private IEnumerator BlendCamerasRoutine()
    {
        _canMove = false;
        _currentCamera = _cameras[_currentCameraIndex];
        _cameras.ForEach(cam => cam.Priority = 0);
        _currentCamera.Priority = 1;
        // Wait a few frames before checking for blend activity
        yield return null;
        yield return null;
        while (_brain.IsBlending)
        {
            yield return null;
        }
        _canMove = true;
    }
}
