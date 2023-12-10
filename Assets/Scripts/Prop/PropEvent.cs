using System;
using UnityEngine;

public class PropEvent
{
    public static event Action<PropHandler> OnPropCollected;

    public static event Action<PropHandler> OnPropCrash;
    public static void RaiseCollected(PropHandler prop, string src)
    {
        Debug.Log($"{prop.name} raised");
        OnPropCollected.CheckSubscriptions(
            prop, src.BuildLogMessage(nameof(RaiseCollected))
        );
    }
    public static void RaiseCrash(PropHandler prop, string src) =>
        OnPropCrash.CheckSubscriptions(
            prop, src.BuildLogMessage(nameof(RaiseCrash))
        );
}
