using System;
using UnityEngine;

    public abstract class EventBase : ScriptableObject
    {
        public event Action OnEventRaised;

        public virtual void Raise(string src) =>
            OnEventRaised.CheckSubscriptions(src.BuildLogMessage(name));
    }

    public abstract class EventBase<T1> : ScriptableObject
    {
        public event Action<T1> OnEventRaised;

        public void RaiseFromUnityEvent(T1 arg1) =>
            Raise(arg1, "A UnityEvent, most likely,");

        public virtual void Raise(T1 arg1, string src) =>
            OnEventRaised.CheckSubscriptions(arg1, src.BuildLogMessage(name));
    }

    public abstract class EventBase<T1, T2> : ScriptableObject
    {
        public event Action<T1, T2> OnEventRaised;

        public virtual void Raise(T1 arg1, T2 arg2, string src) =>
            OnEventRaised.CheckSubscriptions(
                arg1, arg2, src.BuildLogMessage(name)
            );
    }

    public abstract class EventBase<T1, T2, T3> : ScriptableObject
    {
        public event Action<T1, T2, T3> OnEventRaised;

        public virtual void Raise(T1 arg1, T2 arg2, T3 arg3, string src) =>
            OnEventRaised.CheckSubscriptions(
                arg1, arg2, arg3, src.BuildLogMessage(name)
            );
    }

    public abstract class EventBase<T1, T2, T3, T4> : ScriptableObject
    {
        public event Action<T1, T2, T3, T4> OnEventRaised;

        public virtual void Raise(
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, string src
        ) =>
            OnEventRaised.CheckSubscriptions(
                arg1, arg2, arg3, arg4, src.BuildLogMessage(name)
            );
    }
