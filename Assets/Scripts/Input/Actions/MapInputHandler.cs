using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapInputHandler : GameInput.IMapActions
{
    public MapInputHandler(GameInput input)

    {
        input.Map.SetCallbacks(this);
        _gameInput = input;
    }

    public event Action OnCloseMapInput;
    public event Action OnFocusInput;
    public event Action<Vector2> OnPanInput;
    public event Action OnTeleportSelectInput;
    public event Action<bool> OnZoomInInput;
    public event Action<bool> OnZoomOutInput;

    private readonly GameInput _gameInput;

    internal void Enable()
    {
        _gameInput.Atlas.Disable();
        _gameInput.Map.Enable();
        _gameInput.UI.Disable();
    }

    public void Disable() => _gameInput.Map.Disable();

    public void OnCloseMap(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnCloseMapInput);

    public void OnFocus(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnFocusInput);

    public void OnPan(InputAction.CallbackContext c) =>
        c.RaiseInputVector2(OnPanInput);

    public void OnTeleportSelect(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnTeleportSelectInput);

    public void OnZoomIn(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnZoomInInput);

    public void OnZoomOut(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnZoomOutInput);
}
