using System;
using Nerdscape.Events;
using Nerdscape.Events.Logging;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Event_InteractableWaypoint",
    menuName = "Events/Interactable/(Waypoint) event"
)]
public class InteractableWaypointEventSO : ScriptableObject
{
    public event VaultKeyAction<IInteractable> OnEnterRange;
    public event Action<VaultKey> OnExitRange;

    public VaultKey RaiseEnter(IInteractable interactable, string src) =>
        OnEnterRange.CheckKeySubscriptions(
            interactable, src.BuildLogMessage(name)
        );

    public void RaiseExit(VaultKey key, string src) =>
        OnExitRange.CheckSubscriptions(key, src.BuildLogMessage(name));
}
