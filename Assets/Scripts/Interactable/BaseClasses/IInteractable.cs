using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    string InteractPrompt { get; }
    Sprite WaypointImage { get; }
    Vector3 WaypointOffset { get; }
    VaultKey WaypointKey { get; }
    Vector3 Position { get; }
    event UnityAction<IInteractable> OnInteractableDisabled;
    void SetWaypointKey(VaultKey key);
    void Interact();
    void DisableInteractable(bool destroy = false);
}
