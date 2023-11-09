using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuManager : MenuUI
{
    [SerializeField]
    private string _tabHighlightId;

    [SerializeField]
    private SoundEffectAudioDataSO _swapGearSound;

    [SerializeField]
    private ResumeTab _resumeTab;

    [SerializeField]
    private OptionsTab _optionsTab;

    [SerializeField]
    private RestartTab _restartTab;

    [SerializeField]
    private QuitTab _quitTab;

    [SerializeField]
    private PauseTab _currentTab;

    [Header("Broadcasting to...")]
    [SerializeField]
    private VoidEventSO _resetInventoryEvent;

    [SerializeField]
    private LoadSceneEventSO _loadSceneEvent;

    [SerializeField]
    private SceneReferenceSO _reloadScene;

    private VisualElement _highlight;
    
    protected override void OnAwake()
    {
        _highlight = _Root.GetElement(_tabHighlightId);
        _resumeTab.Initialize(_Root, _optionsTab, _resumeTab);
        _optionsTab.Initialize(_Root, _restartTab, _resumeTab);
        _restartTab.Initialize(_Root, _quitTab, _optionsTab);
        _quitTab.Initialize(_Root, _quitTab, _restartTab);
        
        var inventory = _Root.GetElement("c_inventory");
    }

    protected override void OnMenuEnable()
    {
        _Input.Clump.OnPauseInput += HandlePauseInput;
        _Input.UI.OnUnpauseInput += HandleUnpauseInput;
        _resumeTab.OnTabCancel += HandleUnpauseInput;
        _optionsTab.OnTabCancel += HandleUnpauseInput;
        _restartTab.OnTabCancel += HandleUnpauseInput;
        _restartTab.OnRestartRequested += HandleRestartRequest;
        _quitTab.OnTabCancel += HandleUnpauseInput;
    }

    protected override void OnMenuDisable()
    {
        _Input.Clump.OnPauseInput -= HandlePauseInput;
        _Input.UI.OnUnpauseInput -= HandleUnpauseInput;
        _resumeTab.OnTabCancel -= HandleUnpauseInput;
        _optionsTab.OnTabCancel -= HandleUnpauseInput;
        _restartTab.OnTabCancel -= HandleUnpauseInput;
        _restartTab.OnRestartRequested -= HandleRestartRequest;
        _quitTab.OnTabCancel -= HandleUnpauseInput;
    }
    private void HandlePauseInput()
    {
        _currentTab = _resumeTab;
        MoveTabHighlightNext(false);
        OpenMenu();
    }

    protected override void HandleCursorCancel()
    {
        PlaySound(_MenuSounds.Cancel);
        _currentTab.HandleCursorCancel();
    }

    protected override void HandleCursorSelect()
    {
        PlaySound(_MenuSounds.Select);
        _currentTab.HandleCursorSelect();
    }

    protected override void HandleCursorMoveUp() =>
        _currentTab.HandleCursorMoveDown();

    protected override void HandleCursorMoveDown() =>
        _currentTab.HandleCursorMoveDown();

    protected override void HandleCursorMoveLeft()
    {
        if (_currentTab._IsOpen)
        {
            _currentTab.HandleCursorMoveLeft();
            return;
        }
        MoveTabHighlightNext(false);
    }

    protected override void HandleCursorMoveRight()
    {
        if (_currentTab._IsOpen)
        {
            _currentTab.HandleCursorMoveLeft();
            return;
        }
        MoveTabHighlightNext(true);
    }

    protected override void HandleSwapSkills()
    {
        if (_currentTab._IsOpen)
        {
            return;
        }
    }

    protected override void HandleSwapWeapons()
    {
        if (_currentTab._IsOpen)
        {
            return;
        }
    }

    private void PlaySwapGearSound() => PlaySound(_swapGearSound);

    protected override void ResetMenu() { }

    // TODO Probably can go in the resume tab
    private void MoveTabHighlightNext(bool isNext)
    {
        var target = _currentTab.MoveTabHighlight(_highlight, isNext);
        if (_currentTab == target)
        {
            return;
        }
        PlaySound(_MenuSounds.Move);
        _currentTab = target;
    }

    private void HandleUnpauseInput()
    {
        CloseMenu();
    }

    private void HandleRestartRequest()
    {
        CloseMenu();
        // _resetInventoryEvent.Raise(name);
        // _loadSceneEvent.Raise(_reloadScene, true, "Pause screen restart tab");
        SceneManager.LoadScene(_restartTab._SceneName);
    }
}
