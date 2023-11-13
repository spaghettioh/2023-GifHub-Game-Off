using System;

public static class UITweenHandlerExtensions
{
    public static UITweenHandler OnComplete(
        this UITweenHandler tweenRef, Action onComplete = null
    )
    {
        onComplete?.Invoke();
        return tweenRef;
    }

    public static UITweenHandler OnUpdate(
        this UITweenHandler tweenRef, Action onUpdate = null
    )
    {
        onUpdate?.Invoke();
        return tweenRef;
    }
}
