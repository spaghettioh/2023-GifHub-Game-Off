using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
///     This class manages the scene loading and unloading.
/// </summary>
public class SceneLoadManager : MonoBehaviour
{
    [SerializeField]
    private SceneReferenceSO _gameplayManagersScene;

    [Header("Listening for...")]
    [SerializeField]
    private LoadSceneEventSO _loadLocationEvent;

    [SerializeField]
    private LoadSceneEventSO _loadMenuEvent;

    [SerializeField]
    private LoadSceneEventSO _coldStartupEvent;

    [Header("Broadcasting to...")]
    [SerializeField]
    private BoolEventSO _toggleLoadingScreenEvent;

    [SerializeField]
    private VoidEventSO _sceneReadyEvent;

    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
    private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;

    // Parameters coming from scene loading requests
    private SceneReferenceSO _sceneToLoad;
    private SceneReferenceSO _currentlyLoadedScene;
    private bool _showLoadingScreen;
    private SceneInstance _gameplayManagerSceneInstance;

    private void OnEnable()
    {
        _loadLocationEvent.OnEventRaised += HandleLoadLocation;
        _loadMenuEvent.OnEventRaised += HandleLoadMenu;
#if UNITY_EDITOR
        _coldStartupEvent.OnEventRaised += HandleLocationColdStartup;
#endif
    }

    private void OnDisable()
    {
        _loadLocationEvent.OnEventRaised -= HandleLoadLocation;
        _loadMenuEvent.OnEventRaised -= HandleLoadMenu;
#if UNITY_EDITOR
        _coldStartupEvent.OnEventRaised -= HandleLocationColdStartup;
#endif
    }

#if UNITY_EDITOR
    /// <summary>
    ///     This special loading function is only used in the editor, when the
    ///     developer presses Play in a Location scene, without passing by
    ///     Initialisation.
    /// </summary>
    private void HandleLocationColdStartup(
        SceneReferenceSO currentScene, bool showLoadingScreen
    )
    {
        _currentlyLoadedScene = currentScene;

        if (_currentlyLoadedScene.SceneType == SceneReferenceSO.Type.Location)
        {
            //Gameplay managers is loaded synchronously
            _gameplayManagerLoadingOpHandle =
                _gameplayManagersScene.Scene.LoadSceneAsync(
                    LoadSceneMode.Additive
                );
            _gameplayManagerLoadingOpHandle.WaitForCompletion();
            _gameplayManagerSceneInstance =
                _gameplayManagerLoadingOpHandle.Result;

            StartGameplay();
        }
    }
#endif

    /// <summary>
    ///     This function loads the location scenes passed as array parameter
    /// </summary>
    private void HandleLoadLocation(
        SceneReferenceSO locationToLoad, bool showLoadingScreen
    )
    {
        _sceneToLoad = locationToLoad;
        _showLoadingScreen = showLoadingScreen;

        // In case we are coming from the main menu,
        // we need to load the Gameplay manager scene first
        if (_gameplayManagerSceneInstance.Scene == null
            || !_gameplayManagerSceneInstance.Scene.isLoaded)
        {
            _gameplayManagerLoadingOpHandle =
                _gameplayManagersScene.Scene.LoadSceneAsync(
                    LoadSceneMode.Additive
                );
            _gameplayManagerLoadingOpHandle.Completed +=
                HandleGameplayMangersLoaded;
        }
        else
        {
            UnloadPreviousScene();
        }
    }

    private void HandleGameplayMangersLoaded(
        AsyncOperationHandle<SceneInstance> obj
    )
    {
        _gameplayManagerLoadingOpHandle.Completed -=
            HandleGameplayMangersLoaded;
        _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;
        UnloadPreviousScene();
    }

    /// <summary>
    ///     Prepares to load the main menu scene, first removing the Gameplay
    ///     scene in case the game is coming back from gameplay to menus.
    /// </summary>
    private void HandleLoadMenu(
        SceneReferenceSO menuToLoad, bool showLoadingScreen
    )
    {
        _sceneToLoad = menuToLoad;
        _showLoadingScreen = showLoadingScreen;

        // In case we are coming from a Location back to the main menu, we need
        // to get rid of the persistent Gameplay manager scene
        if (_gameplayManagerSceneInstance.Scene != null
            && _gameplayManagerSceneInstance.Scene.isLoaded)
        {
            Addressables.UnloadSceneAsync(_gameplayManagerLoadingOpHandle);
        }

        UnloadPreviousScene();
    }

    /// <summary>
    ///     In both Location and Menu loading, this function takes care of
    ///     removing previously loaded scenes.
    /// </summary>
    private void UnloadPreviousScene()
    {
        // would be null if the game was started in Initialisation
        if (_currentlyLoadedScene != null)
        {
            if (_currentlyLoadedScene.Scene.OperationHandle.IsValid())
            {
                // Unload the scene through its AssetReference,
                // i.e. through the Addressable system
                _loadingOperationHandle =
                    _currentlyLoadedScene.Scene.UnLoadScene();
            }
#if UNITY_EDITOR
            else
            {
                // Only used when, after a "cold start", the player moves to a
                // new scene
                // Since the AsyncOperationHandle has not been used (the scene
                // was already open in the editor),
                // the scene needs to be unloaded using regular SceneManager
                // instead of as an Addressable
                _currentlyLoadedScene.Scene.UnLoadScene();
                // SceneManager.UnloadSceneAsync(
                //     _currentlyLoadedScene.Scene.editorAsset.name
                // );
            }
#endif
        }

        LoadNewScene();
    }

    /// <summary>
    ///     Kicks off the asynchronous loading of a scene, either menu or
    ///     Location.
    /// </summary>
    private void LoadNewScene()
    {
        if (_showLoadingScreen)
        {
            _toggleLoadingScreenEvent.Raise(true, name);
        }

        _loadingOperationHandle = _sceneToLoad.Scene.LoadSceneAsync(
            LoadSceneMode.Additive, true, 0
        );
        _loadingOperationHandle.Completed += OnNewSceneLoaded;
    }

    private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _loadingOperationHandle.Completed -= OnNewSceneLoaded;
        // Save loaded scenes (to be unloaded at next load request)
        _currentlyLoadedScene = _sceneToLoad;
        SetActiveScene();

        if (_showLoadingScreen)
        {
            _toggleLoadingScreenEvent.Raise(false, name);
        }
    }

    /// <summary>
    ///     This function is called when all the scenes have been loaded
    /// </summary>
    private void SetActiveScene()
    {
        var s = _loadingOperationHandle.Result.Scene;
        SceneManager.SetActiveScene(s);
        StartGameplay();
    }

    private void StartGameplay()
    {
        _sceneReadyEvent.Raise(name);
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }
}
