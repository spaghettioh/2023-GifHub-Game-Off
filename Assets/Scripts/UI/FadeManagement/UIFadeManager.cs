// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;

// public class UIFadeManager : MonoBehaviour
// {
//     [SerializeField]
//     private UIFadeEventSO _fadeEvent;

//     private void OnEnable()
//     {
//         _fadeEvent.OnFadeIn += FadeIn;
//         _fadeEvent.OnFadeOut += FadeOut;
//         _fadeEvent.OnFadeInAllExcept += OnFadeInAllExcept;
//         _fadeEvent.OnFadeOutAllExcept += OnFadeOutAllExcept;
//     }

//     private void OnDisable()
//     {
//         _fadeEvent.OnFadeIn -= FadeIn;
//         _fadeEvent.OnFadeOut -= FadeOut;
//         _fadeEvent.OnFadeInAllExcept -= OnFadeInAllExcept;
//         _fadeEvent.OnFadeOutAllExcept -= OnFadeOutAllExcept;
//     }

//     private void FadeIn(IUIFadable element)
//     {
//         element.FadeIn();
//     }

//     private void FadeOut(IUIFadable element)
//     {
//         element.FadeOut();
//     }

//     private void OnFadeInAllExcept(IUIFadable element)
//     {
//         if (element != this)
//         {
//             FadeIn(element);
//         }
//     }

//     private void OnFadeOutAllExcept(IUIFadable element)
//     {
//         if (element != this)
//         {
//             FadeOut(element);
//         }
//     }
// }
