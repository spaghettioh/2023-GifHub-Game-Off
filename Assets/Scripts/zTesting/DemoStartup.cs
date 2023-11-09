using UnityEngine;

public class DemoStartup : MonoBehaviour
{
    [SerializeField]
    private VoidEventSO _sceneLoadedEvent;
    private void Start()
    {
        _sceneLoadedEvent.Raise(name);
    }
}
