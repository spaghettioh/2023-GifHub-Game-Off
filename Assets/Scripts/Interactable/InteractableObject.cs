using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UnityEvent _onInteract;

    [field: SerializeField]
    public string InteractPrompt { get; private set; }

    [field: SerializeField]
    public Sprite WaypointImage { get; private set; }
    public Vector3 WaypointOffset => Vector3.zero;
    public VaultKey WaypointKey { get; private set; }
    private Transform _t;
    public Vector3 Position => _t.position;
    public event UnityAction<IInteractable> OnInteractableDisabled;

    private void Awake()
    {
        TryGetComponent(out _t);
    }

    public void SetWaypointKey(VaultKey key)
    {
        WaypointKey = key;
    }
    public void Interact()
    {
        _onInteract.Invoke();
    }
    public void DisableInteractable(bool destroy = false)
    {
        // throw new System.NotImplementedException();
    }
}
