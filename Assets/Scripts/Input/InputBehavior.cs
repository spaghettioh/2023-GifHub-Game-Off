using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;

public class InputBehavior : MonoBehaviour
{
    [FormerlySerializedAs("_inputHandler")]
    [SerializeField]
    private InputEventSO _inputEvent;

    private void OnEnable()
    {
        InputUser.onChange += InputUserOnChange;
    }

    private void OnDisable()
    {
        InputUser.onChange -= InputUserOnChange;
    }

    private void InputUserOnChange(
        InputUser arg1,
        InputUserChange arg2,
        InputDevice arg3
    )
    {
        _inputEvent.OnInputDeviceChanged(arg1, arg2, arg3);
    }
}
