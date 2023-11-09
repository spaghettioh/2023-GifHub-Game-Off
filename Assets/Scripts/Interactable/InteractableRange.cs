using UnityEngine;

public class InteractableRange : MonoBehaviour
{
    [SerializeField]
    private InteractableRangeEventSO _interactableEvent;

    private void OnTriggerEnter(Collider collider)
    {
        if (TryGetInteractable(collider, out var interactable))
        {
            EnterRange(interactable);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (TryGetInteractable(collider, out var interactable))
        {
            ExitRange(interactable);
        }
    }

    private void EnterRange(IInteractable interactable) =>
        _interactableEvent.RaiseEnter(interactable, name);

    private void ExitRange(IInteractable interactable) =>
        _interactableEvent.RaiseExit(interactable, name);

    private static bool TryGetInteractable(
        Component collider, out IInteractable interactable
    ) =>
        collider.TryGetComponent(out interactable);
}
