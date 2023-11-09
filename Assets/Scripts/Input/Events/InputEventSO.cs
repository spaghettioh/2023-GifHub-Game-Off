using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[CreateAssetMenu(
    fileName = "InputHandler", menuName = "Runtime/Input/Input handler"
)]
public class InputEventSO : ScriptableObject
{
    private GameInput _gameInput;
    public ClumpInputHandler Clump;
    public MapInputHandler Map;
    public UIInputHandler UI;
    private InputControlScheme _controlScheme;
    public bool IsGamepad => _controlScheme == _gameInput.GamepadScheme;
    public bool IsKeyboardMouse =>
        _controlScheme == _gameInput.KeyboardMouseScheme;

    public void EnableAtlas() => Clump.Enable();

    public void EnableUI() => UI.Enable();

    public void EnableMap() => Map.Enable();

    public void DisableAllInput()
    {
        Clump.Disable();
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
        Clump = new(_gameInput);
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
