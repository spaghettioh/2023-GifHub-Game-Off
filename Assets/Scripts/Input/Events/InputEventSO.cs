using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[CreateAssetMenu(
    fileName = "InputHandler", menuName = "Runtime/Input/Input handler"
)]
public class InputEventSO : ScriptableObject
{
    private GameInput _gameInput;
    public AtlasInputHandler Atlas;
    public MapInputHandler Map;
    public UIInputHandler UI;
    private InputControlScheme _controlScheme;
    public bool IsGamepad => _controlScheme == _gameInput.GamepadScheme;
    public bool IsKeyboardMouse =>
        _controlScheme == _gameInput.KeyboardMouseScheme;

    public void EnableGameplay() => Atlas.Enable();

    public void EnableUI() => UI.Enable();

    public void EnableMap() => Map.Enable();

    public void DisableAllInput()
    {
        Atlas.Disable();
        Map.Disable();
        UI.Disable();
    }

    private void OnEnable()
    {
        if (_gameInput != null)
        {
            return;
        }

        _gameInput = new();
        Atlas = new(_gameInput);
        Map = new(_gameInput);
        UI = new(_gameInput);
    }

    public void OnInputDeviceChanged(
        InputUser user, InputUserChange change, InputDevice device
    )
    {
        // BUG only works when its different
        if (change == InputUserChange.ControlSchemeChanged)
        {
            _controlScheme = user.controlScheme.Value;
        }
    }
}
