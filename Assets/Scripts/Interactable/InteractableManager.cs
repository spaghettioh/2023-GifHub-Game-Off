using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractableManager : MonoBehaviour
{
    [Header("Listening for...")]
    [SerializeField]
    private InteractableRangeEventSO _sightRangeEvent;

    [SerializeField]
    private InteractableRangeEventSO _interactableRangeEvent;

    [SerializeField]
    private InputEventSO _inputEvent;
    
    [SerializeField]
    private TransformAnchorSO _transformAnchor;

    [Header("Broadcasting to...")]
    [SerializeField]
    private InteractableWaypointEventSO _waypointEvent;

    [SerializeField]
    private InteractableClosestEventSO _interactableClosestEvent;

    public List<IInteractable> SightRange { get; } = new();
    public List<IInteractable> InteractableRange { get; } = new();
    public IInteractable Closest { get; private set; }

    private void OnEnable()
    {
        _sightRangeEvent.OnEnterRange += HandleEnterSightRange;
        _sightRangeEvent.OnExitRange += HandleExitSightRange;

        _interactableRangeEvent.OnEnterRange += HandleEnterInteractableRange;
        _interactableRangeEvent.OnExitRange += HandleExitInteractableRange;

        _inputEvent.Clump.OnInteractInput += HandleInteractInput;
    }

    private void OnDisable()
    {
        _sightRangeEvent.OnEnterRange -= HandleEnterSightRange;
        _sightRangeEvent.OnExitRange -= HandleExitSightRange;

        _interactableRangeEvent.OnEnterRange -= HandleEnterInteractableRange;
        _interactableRangeEvent.OnExitRange -= HandleExitInteractableRange;

        _inputEvent.Clump.OnInteractInput -= HandleInteractInput;
    }

    private void Update()
    {
        if (InteractableRange.Count == 0)
        {
            return;
        }

        IOrderedEnumerable<IInteractable> ordered = InteractableRange.OrderBy(
            item => Vector3.Distance(
                item.Position, _transformAnchor.Transform.position
            )
        );
        var closest = ordered.FirstOrDefault();

        if (!Closest.IsNull())
        {
            if (Closest.Equals(closest))
            {
                return;
            }
            _interactableClosestEvent.Raise(Closest.WaypointKey, false, name);
        }

        Closest = closest;
        _interactableClosestEvent.Raise(Closest.WaypointKey, true, name);
    }

    private void HandleEnterSightRange(IInteractable interactable)
    {
        var key = _waypointEvent.RaiseEnter(interactable, name);
        SightRange.Add(interactable);
        interactable.SetWaypointKey(key);
        interactable.OnInteractableDisabled += HandleInteractableDisabled;
    }

    private void HandleEnterInteractableRange(IInteractable interactable) =>
        InteractableRange.Add(interactable);

    private void HandleInteractableDisabled(IInteractable interactable)
    {
        interactable.OnInteractableDisabled -= HandleInteractableDisabled;
        HandleExitInteractableRange(interactable);
        HandleExitSightRange(interactable);
    }

    private void HandleInteractInput()
    {
        if (Closest.IsNull())
        {
            return;
        }

        Closest.Interact();
    }

    private void HandleExitInteractableRange(IInteractable interactable)
    {
        if (Closest == interactable)
        {
            _interactableClosestEvent.Raise(Closest.WaypointKey, false, name);
            Closest = null;
        }
        InteractableRange.Remove(interactable);
    }

    private void HandleExitSightRange(IInteractable interactable)
    {
        SightRange.Remove(interactable);
        _waypointEvent.RaiseExit(interactable.WaypointKey, name);
    }
}
