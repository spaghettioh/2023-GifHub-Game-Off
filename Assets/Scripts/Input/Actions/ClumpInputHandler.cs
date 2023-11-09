using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClumpInputHandler : GameInput.IClumpActions
{
    public ClumpInputHandler(GameInput input)

    {
        input.Clump.SetCallbacks(this);
        _gameInput = input;
    }

    public event Action<bool> OnBankLeftInput;
    public event Action<bool> OnBankRightInput;
    public event Action<bool> OnBoostInput;
    public event Action<bool> OnBrakeInput;
    public event Action<bool> OnHealInput;
    public event Action OnInteractInput;
    public event Action<Vector2> OnLookInput;
    public event Action<Vector2> OnMovementGamepadInput;
    public event Action OnOpenMapInput;
    public event Action OnPauseInput;
    public event Action<bool> OnSkillPrimaryInput;
    public event Action<bool> OnSkillSecondaryInput;
    public event Action<bool> OnWeaponPrimaryInput;
    public event Action<bool> OnWeaponSecondaryInput;

    private readonly GameInput _gameInput;

    internal void Enable()
    {
        _gameInput.Clump.Enable();
        _gameInput.Map.Disable();
        _gameInput.UI.Disable();
    }

    public void Disable() => _gameInput.Clump.Disable();

    public void OnCameraLeft(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnPauseInput);

    public void OnCameraRight(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnPauseInput);

    public void OnMovement(InputAction.CallbackContext c) =>
        c.RaiseInputVector2(OnMovementGamepadInput);

    public void OnPause(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnPauseInput);
}
