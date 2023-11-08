using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtlasInputHandler : GameInput.IAtlasActions
{
    public AtlasInputHandler(GameInput input)

    {
        input.Atlas.SetCallbacks(this);
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
        _gameInput.Atlas.Enable();
        _gameInput.Map.Disable();
        _gameInput.UI.Disable();
    }

    public void Disable() => _gameInput.Atlas.Disable();

    public void OnBankLeft(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnBankLeftInput);

    public void OnBankRight(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnBankRightInput);

    public void OnBoost(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnBoostInput);

    public void OnBrake(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnBrakeInput);

    public void OnHeal(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnHealInput);

    public void OnInteract(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnInteractInput);

    public void OnLook(InputAction.CallbackContext c) =>
        c.RaiseInputVector2(OnLookInput);

    public void OnMovementGamepad(InputAction.CallbackContext c) =>
        // FR I want to invert my up/down flight controls
        // "Invert Y" option goes here?
        c.RaiseInputVector2(OnMovementGamepadInput);

    public void OnMovementMouse(InputAction.CallbackContext c)
    {
        // RaiseInputVector2(c, OnMovementGamepadInput);
    }

    public void OnOpenMap(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnOpenMapInput);

    public void OnPause(InputAction.CallbackContext c) =>
        c.RaiseInputStarted(OnPauseInput);

    public void OnSkillPrimary(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnSkillPrimaryInput);

    public void OnSkillSecondary(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnSkillSecondaryInput);

    public void OnWeaponPrimary(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnWeaponPrimaryInput);

    public void OnWeaponSecondary(InputAction.CallbackContext c) =>
        c.RaiseInputPerformed(OnWeaponSecondaryInput);
}
