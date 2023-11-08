using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputExtensions
{
    public static void RaiseInputPerformed(
        this InputAction.CallbackContext context, Action<bool> action
    )
    {
        if (context.started)
        {
            return;
        }

        action?.Invoke(context.performed);
    }

    public static void RaiseInputStarted(
        this InputAction.CallbackContext context, Action action
    )
    {
        if (context.started)
        {
            action?.Invoke();
        }
    }

    public static void RaiseInputStarted(
        this InputAction.CallbackContext context, bool condition, Action action
    )
    {
        if (condition)
        {
            RaiseInputStarted(context, action);
        }
    }

    public static void RaiseInputVector2(
        this InputAction.CallbackContext context, Action<Vector2> action
    ) =>
        action?.Invoke(context.ReadValue<Vector2>());

    public static void RaiseInputVector2(
        this InputAction.CallbackContext context, bool condition,
        Action<Vector2> action
    )
    {
        if (condition)
        {
            RaiseInputVector2(context, action);
        }
    }
}
