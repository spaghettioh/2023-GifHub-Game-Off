using UnityEngine;

public class ReloadManager : MonoBehaviour
{
    [SerializeField]
    private VoidEventSO _resetInventoryEvent;

    [SerializeField]
    private LoadSceneEventSO _loadSceneEvent;

    [SerializeField]
    private SceneReferenceSO _sceneToReload;

    private void Start()
    {
        _resetInventoryEvent.Raise(name);
        _loadSceneEvent.Raise(_sceneToReload, true, name);
    }
}
