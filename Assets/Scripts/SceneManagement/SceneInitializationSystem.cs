using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
///     Allows a "cold start" in the editor, when pressing Play and not passing
///     from
///     the Initialisation scene.
/// </summary>
public class SceneInitializationSystem : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private SceneReferenceSO _thisScene;

    [Header("Prefab config")]
    [SerializeField]
    private SceneReferenceSO _persistentManagers;

    [SerializeField]
    private AssetReference _notifyColdStartupChannel;

    private void Start()
    {
        if (!SceneManager
                .GetSceneByName(_persistentManagers.Scene.editorAsset.name)
                .isLoaded)
        {
            _persistentManagers.Scene.LoadSceneAsync(LoadSceneMode.Additive)
                .Completed += LoadEventChannel;
        }
    }

    private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj) =>
        _notifyColdStartupChannel.LoadAssetAsync<LoadSceneEventSO>()
            .Completed += OnNotifyChannelLoaded;

    private void OnNotifyChannelLoaded(
        AsyncOperationHandle<LoadSceneEventSO> obj
    )
    {
        var loadEventChannel =
            (LoadSceneEventSO)_notifyColdStartupChannel.Asset;
        loadEventChannel.Raise(_thisScene, false, name);
    }

    private void OnValidate()
    {
        if (gameObject.activeInHierarchy && _thisScene == null)
        {
            Debug.LogError($"{name}: {nameof(_thisScene)} can't be empty!");
        }
    }
#endif
}
