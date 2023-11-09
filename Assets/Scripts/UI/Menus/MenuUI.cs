using UnityEngine;

public abstract class MenuUI : UI
{
    [SerializeField]
    protected MenuSoundsSO _MenuSounds;

    [SerializeField]
    protected InputEventSO _Input;

    [Header("Broadcasting to...")]
    [SerializeField]
    private AudioEventSO _audioEvent;

    [field: SerializeField]
    public UIFadeEventSO FadeEvent { get; set; }

    protected override void Awake()
    {
        base.Awake();
        OnAwake();
    }
    protected virtual void OnAwake() { }

    private void OnEnable() => OnMenuEnable();

    private void OnDisable()
    {
        OnMenuDisable();
        ResetMenu();
        UnsubscribeFromInput();
    }

    /// <summary>
    ///     Use to subscribe to any events specific to this menu.
    /// </summary>
    protected virtual void OnMenuEnable() { }

    /// <summary>
    ///     Use to unsubscribe to any events specific to this menu.
    /// </summary>
    protected virtual void OnMenuDisable() { }

    protected virtual void SubscribeToInput()
    {
        _Input.UI.OnCancelInput += HandleCursorCancel;
        _Input.UI.OnMoveCursorDownInput += HandleCursorMoveDown;
        _Input.UI.OnMoveCursorLeftInput += HandleCursorMoveLeft;
        _Input.UI.OnMoveCursorRightInput += HandleCursorMoveRight;
        _Input.UI.OnMoveCursorUpInput += HandleCursorMoveUp;
        _Input.UI.OnSelectInput += HandleCursorSelect;
        _Input.UI.OnSwapSkillsInput += HandleSwapSkills;
        _Input.UI.OnSwapWeaponsInput += HandleSwapWeapons;
    }

    protected virtual void UnsubscribeFromInput()
    {
        _Input.UI.OnCancelInput -= HandleCursorCancel;
        _Input.UI.OnMoveCursorDownInput -= HandleCursorMoveDown;
        _Input.UI.OnMoveCursorLeftInput -= HandleCursorMoveLeft;
        _Input.UI.OnMoveCursorRightInput -= HandleCursorMoveRight;
        _Input.UI.OnMoveCursorUpInput -= HandleCursorMoveUp;
        _Input.UI.OnSelectInput -= HandleCursorSelect;
        _Input.UI.OnSwapSkillsInput -= HandleSwapSkills;
        _Input.UI.OnSwapWeaponsInput -= HandleSwapWeapons;
    }

    protected virtual void OpenMenu()
    {
        SubscribeToInput();
        Time.timeScale = 0f;
        _audioEvent.RaiseOpenMenu(true, name);
        _Input.EnableUI();
        ShowUI();
        PlaySound(_MenuSounds.Open);
    }

    protected virtual void CloseMenu()
    {
        UnsubscribeFromInput();
        Time.timeScale = 1f;
        _audioEvent.RaiseOpenMenu(false, name);
        HideUI();
        _Input.EnableAtlas();
        ResetMenu();
    }

    protected virtual void HandleCursorCancel() { }
    protected virtual void HandleCursorMoveDown() { }
    protected virtual void HandleCursorMoveLeft() { }
    protected virtual void HandleCursorMoveRight() { }
    protected virtual void HandleCursorMoveUp() { }
    protected virtual void HandleCursorSelect() { }

    protected virtual void HandleSwapSkills() { }

    protected virtual void HandleSwapWeapons() { }

    protected virtual void PlaySound(SoundEffectAudioDataSO sound) =>
        _audioEvent.RaisePlayback(sound, name);

    protected abstract void ResetMenu();
}
