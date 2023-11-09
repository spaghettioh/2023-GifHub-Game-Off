using System;
using UnityEngine;

[Serializable]
public class MapMenuCameraController
{
    [SerializeField]
    private Transform _mapMenuCamera;

    [SerializeField]
    private float _moveSpeed = 750f;

    [SerializeField]
    private InputEventSO _input;

    [SerializeField]
    private ClumpDataSO _runtimeData;
    private float _centerY;
    private Vector3 _max;

    private Vector3 _min;
    private Vector3 _moveDirection = Vector3.zero;
    internal bool IsMenuOpen { get; private set; }
    private bool _isFogEnabled;

    internal void OnManagerEnabled()
    {
        _input.Clump.OnOpenMapInput += HandleOpenMap;
        _input.Map.OnPanInput += HandlePan;
        _input.Map.OnFocusInput += HandleFocus;
        _input.Map.OnZoomInInput += HandleZoomIn;
        _input.Map.OnZoomOutInput += HandleZoomOut;
        _input.Map.OnCloseMapInput += HandleCloseMap;
    }

    internal void OnManagerDisabled()
    {
        _input.Clump.OnOpenMapInput -= HandleOpenMap;
        _input.Map.OnPanInput -= HandlePan;
        _input.Map.OnFocusInput -= HandleFocus;
        _input.Map.OnZoomInInput -= HandleZoomIn;
        _input.Map.OnZoomOutInput -= HandleZoomOut;
        _input.Map.OnCloseMapInput -= HandleCloseMap;
    }

    internal void Initialize(Vector3 min, Vector3 max, float centerY)
    {
        _min = min;
        _max = max;
        _centerY = centerY;
        _isFogEnabled = RenderSettings.fog;
        HandleCloseMap();
    }

    private void HandleOpenMap()
    {
        IsMenuOpen = true;
        RenderSettings.fog = false;
        _mapMenuCamera.gameObject.SetActive(true);
        ResetPosition();
    }

    private void HandleCloseMap()
    {
        IsMenuOpen = false;
        _mapMenuCamera.gameObject.SetActive(false);
        RenderSettings.fog = _isFogEnabled;
    }

    private void HandlePan(Vector2 input)
    {
        _moveDirection.x = input.x;
        _moveDirection.z = input.y;
    }

    private void HandleFocus() => ResetPosition(true);

    private void HandleZoomIn(bool performed) =>
        _moveDirection.y = performed ? -1f : 0f;

    private void HandleZoomOut(bool performed) =>
        _moveDirection.y = performed ? 1f : 0f;

    internal void UpdateCameraPosition()
    {
        var newPosition = GetNewPosition();
        SetPosition(newPosition);
    }

    private Vector3 GetNewPosition()
    {
        var moveAmount = _moveDirection * (_moveSpeed * Time.unscaledDeltaTime);
        var currentPosition = _mapMenuCamera.position + moveAmount;

        currentPosition.x = Mathf.Clamp(currentPosition.x, _min.x, _max.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, _min.y, _max.y);
        currentPosition.z = Mathf.Clamp(currentPosition.z, _min.z, _max.z);
        return currentPosition;
    }

    private void ResetPosition(bool focus = false)
    {
        var x = _runtimeData.Position.x;
        var y = focus ? _mapMenuCamera.position.y : _centerY;
        var z = _runtimeData.Position.z;
        SetPosition(new(x, y, z));
    }

    private void SetPosition(Vector3 position)
    {
        var target = Vector3Int.RoundToInt(position);
        _mapMenuCamera.position = target;
    }
}
