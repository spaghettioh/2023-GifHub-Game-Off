using System;

public abstract class StaticEventBase<T>
{
    public static event Action OnEventRaised;

    public static void Raise(string elevator)
    {
        OnEventRaised.CheckSubscriptions(elevator);
    }
}
