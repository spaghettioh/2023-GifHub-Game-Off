using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector2> OnDirectionalInput;

    [SerializeField]
    private InputEventSO _inputEvent;

    [SerializeField]
    private Joystick _westJoystick;

    [SerializeField]
    private Joystick _eastJoystick;

    private Vector2 _directionalInput;
    private Vector2 _westTouch;
    private Vector2 _eastTouch;

    private void Update()
    {
        _directionalInput = new(
            Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")
        );
        // _westTouch = new(_westJoystick.Horizontal, _westJoystick.Vertical);
        //
        // _eastTouch = new(_eastJoystick.Horizontal, _eastJoystick.Vertical);

        // if (_directionalInput != Vector2.zero)
        // {
        OnDirectionalInput.CheckSubscriptions(_directionalInput, name);
        // }
        // else if (_westTouch != Vector2.zero)
        // {
        //     OnDirectionalInput.CheckSubscriptions(_westTouch, name);
        // }
        // else if (_eastTouch != Vector2.zero)
        // {
        //     OnDirectionalInput.CheckSubscriptions(_eastTouch, name);
        // }
        // else
        // {
        //     OnDirectionalInput.CheckSubscriptions(Vector2.zero, name);
        // }
    }
}
