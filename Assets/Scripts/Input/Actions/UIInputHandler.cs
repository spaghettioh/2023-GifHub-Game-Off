using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : GameInput.IUIActions
{
    public UIInputHandler(GameInput input)

    {
        input.UI.SetCallbacks(this);
        _gameInput = input;
    }

    public event Action OnCancelInput;
    public event Action OnMoveCursorUpInput;
    public event Action OnMoveCursorDownInput;
    public event Action OnMoveCursorLeftInput;
    public event Action OnMoveCursorRightInput;
    public event Action OnSelectInput;
    public event Action OnSwapSkillsInput;
    public event Action OnSwapWeaponsInput;
    public event Action OnUnpauseInput;

    private readonly GameInput _gameInput;

    internal void Enable()
    {
        _gameInput.Clump.Disable();
        _gameInput.Map.Disable();
        _gameInput.UI.Enable();
    }

    public void Disable() => _gameInput.UI.Disable();

    public void OnCancel(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnCancelInput);

    public void OnMoveCursor(InputAction.CallbackContext c)
    {
        var input = c.ReadValue<Vector2>();
        c.RaiseInputStarted(input.x > 0, OnMoveCursorRightInput);
        c.RaiseInputStarted(input.x < 0, OnMoveCursorLeftInput);
        c.RaiseInputStarted(input.y > 0, OnMoveCursorUpInput);
        c.RaiseInputStarted(input.y < 0, OnMoveCursorDownInput);
    }

    public void OnSelect(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnSelectInput);

    public void OnUnpause(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnUnpauseInput);
}
