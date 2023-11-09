using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Event_UIFade", menuName = "Events/UI/Fade event")]
public class UIFadeEventSO : ScriptableObject
{
    public event FadeInAction<IUIFadable> OnFadeIn;
    public event FadeOutAction<IUIFadable> OnFadeOut;

    public void FadeIn<T>(string elevator = "Unknown")
        where T : IUIFadable
    {
        //         if (OnFadeIn != null)
        //         {
        //             OnFadeIn.Invoke(typeof(T) as IUIFadable);
        //         }
        // #if UNITY_EDITOR
        //         else
        //         {
        //             Debug.LogWarning(
        //                 $"{elevator} raised {nameof(FadeIn)}() in {name} but no one listens."
        //             );
        //         }
        // #endif
    }

    public void FadeOut<T>(string elevator = "Unknown")
        where T : IUIFadable
    {
        //         if (OnFadeOut != null)
        //         {
        //             OnFadeOut.Invoke(typeof(T) as IUIFadable);
        //         }
        // #if UNITY_EDITOR
        //         else
        //         {
        //             Debug.LogWarning(
        //                 $"{elevator} raised {nameof(FadeOut)}() in {name} but no one listens."
        //             );
        //         }
        // #endif
    }
}

public delegate void FadeInAction<T>(IUIFadable ui);
public delegate void FadeOutAction<T>(IUIFadable ui);
