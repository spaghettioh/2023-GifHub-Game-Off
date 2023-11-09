using System;
using Nerdscape.Events;
using Nerdscape.Events.Logging;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Event_InteractableRange_NAME",
    menuName = "Events/Interactable/(Range) event"
)]
public class InteractableRangeEventSO : ScriptableObject
{
    public event Action<IInteractable> OnEnterRange;
    public event Action<IInteractable> OnExitRange;

    public void RaiseEnter(IInteractable interactable, string src) =>
        OnEnterRange.CheckSubscriptions(
            interactable, src.BuildLogMessage(name)
        );

    public void RaiseExit(IInteractable interactable, string src) =>
        OnExitRange.CheckSubscriptions(interactable, src.BuildLogMessage(name));
}
