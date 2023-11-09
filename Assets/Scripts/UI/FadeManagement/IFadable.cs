using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIFadable
{
    UIFadeEventSO FadeEvent { get; }
    void FadeIn(IUIFadable ui);
    void FadeOut(IUIFadable ui);
}
