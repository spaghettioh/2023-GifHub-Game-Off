using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class GameInitializationSystem : MonoBehaviour
{
    [SerializeField]
    private SceneReferenceSO _persistentManagersScene;
    [SerializeField]
    private SceneReferenceSO _firstScene;

    [Header("Broadcasting to...")]
    [SerializeField]
    private AssetReference _sceneLoadEvent;

    private void Start() =>
        _persistentManagersScene.Scene.LoadSceneAsync(LoadSceneMode.Additive)
            .Completed += LoadEventChannel;

    private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
    {
        _sceneLoadEvent.LoadAssetAsync<LoadSceneEventSO>().Completed +=
            LoadMainMenu;
    }

    private void LoadMainMenu(AsyncOperationHandle<LoadSceneEventSO> obj)
    {
        var loadEventChannel = (LoadSceneEventSO)_sceneLoadEvent.Asset;
        loadEventChannel.Raise(_firstScene, true, name);

        // Initialization is the only scene in BuildSettings,
        // thus it has index 0
        SceneManager.UnloadSceneAsync(0);
    }
}
