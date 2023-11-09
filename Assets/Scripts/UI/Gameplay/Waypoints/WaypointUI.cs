using UnityEngine;

public class WaypointUI : UI
{
    [SerializeField]
    private InteractableWaypointEventSO _waypointEvent;

    [SerializeField]
    private InteractableClosestEventSO _closestEvent;

    [Header("Listening to...")]
    [SerializeField]
    private TransformEventSO _atlasSpawned;

    [SerializeField]
    private WaypointPoolSO _waypointTemplatePool;

    private readonly Vault<IInteractable, Waypoint> _waypointVault = new();

    protected override void Awake()
    {
        base.Awake();
        _waypointTemplatePool.PreWarm(_Root);
    }

    private void OnEnable()
    {
        _waypointEvent.OnEnterRange += HandleEnterSightRange;
        _waypointEvent.OnExitRange += HandleExitSightRange;

        _closestEvent.OnEventRaised += OnIsClosest;
    }

    private void OnDisable()
    {
        _waypointEvent.OnEnterRange -= HandleEnterSightRange;
        _waypointEvent.OnExitRange -= HandleExitSightRange;

        _closestEvent.OnEventRaised -= OnIsClosest;
    }

    protected override void OnUIStart() => ShowUI();

    private void Update() =>
        _waypointVault.Entities.ForEach(e => e.UpdatePosition());

    private VaultKey HandleEnterSightRange(IInteractable interactable)
    {
        var template = _waypointTemplatePool.Request();
        var waypoint = new Waypoint(interactable, template);
        waypoint.UpdatePosition();
        waypoint.SetWaypointVisible(true);
        var key = _waypointVault.Add(interactable, waypoint);
        return key;
    }

    private void HandleExitSightRange(VaultKey key)
    {
        if (!_waypointVault.TryGetEntity(key, out var waypoint))
        {
            return;
        }
        _waypointVault.Remove(key);
        _waypointTemplatePool.Return(waypoint.Template);
    }

    private void OnIsClosest(VaultKey key, bool isClosest)
    {
        if (_waypointVault.TryGetEntity(key, out var waypoint))
        {
            waypoint.SetPromptVisible(isClosest);
        }
    }
}
