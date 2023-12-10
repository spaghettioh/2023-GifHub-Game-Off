using System;

public static class EventExtensions
{
    public static void CheckSubscriptions(this Action action, string msg)
    {
        if (action == null)
        {
            msg.LogWarning();
            return;
        }
        msg.Log();
        action.Invoke();
    }

    public static void CheckSubscriptions<T>(
        this Action<T> action, T arg, string msg
    )
    {
        if (action == null)
        {
            msg.LogWarning();
            return;
        }
        msg.Log();
        action.Invoke(arg);
    }

    public static void CheckSubscriptions<T1, T2>(
        this Action<T1, T2> action, T1 arg1, T2 arg2, string msg
    )
    {
        if (action == null)
        {
            msg.LogWarning();
            return;
        }
        msg.Log();
        action.Invoke(arg1, arg2);
    }

    public static void CheckSubscriptions<T1, T2, T3>(
        this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3,
        string msg
    )
    {
        if (action == null)
        {
            msg.LogWarning();
            return;
        }
        msg.Log();
        action.Invoke(arg1, arg2, arg3);
    }

    public static void CheckSubscriptions<T1, T2, T3, T4>(
        this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3,
        T4 arg4, string msg
    )
    {
        if (action == null)
        {
            msg.LogWarning();
            return;
        }
        msg.Log();
        action.Invoke(arg1, arg2, arg3, arg4);
    }
}