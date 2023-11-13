using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputEvent", menuName = "Events/Input event")]
public class InputEventSO : ScriptableObject
{
    public event Action<Vector2> OnDirectionalInput;
    public Vector2 DirectionalInput;

    public void SetDirectionalInput(Vector2 input, string elevator)
    {
        OnDirectionalInput.CheckSubscriptions(input, elevator);
        DirectionalInput = input;
    }
}
