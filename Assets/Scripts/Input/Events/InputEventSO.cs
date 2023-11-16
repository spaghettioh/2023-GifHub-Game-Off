using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputEvent", menuName = "Events/Input event")]
public class InputEventSO : ScriptableObject
{
    public event Action<Vector2> OnMovementInput;
    public Vector2 DirectionalInput;

    public void SetDirectionalInput(Vector2 input, string elevator)
    {
        OnMovementInput.CheckSubscriptions(input, elevator);
        DirectionalInput = input;
    }
}
