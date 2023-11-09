using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public abstract class PauseTab
{
    public event Action OnTabCancel;

    [SerializeField]
    private string _tabId;

    [SerializeField]
    private VisualTreeAsset _screenAsset;

    protected VisualElement Root;
    public VisualElement TabElement { get; private set; }
    public PauseTab NextTab { get; private set; }
    public PauseTab PreviousTab { get; private set; }
    protected VisualElement _Screen;

    public bool _IsOpen { get; protected set; }

    internal virtual void Initialize(
        VisualElement root,
        PauseTab nextTab,
        PauseTab previousTab
    )
    {
        Root = root;
        TabElement = root.GetElement(_tabId);
        NextTab = nextTab;
        PreviousTab = previousTab;
    }

    protected void RaiseCancelEvent() => OnTabCancel.Invoke();

    public virtual void HandleCursorMoveUp() { }

    public virtual void HandleCursorMoveDown() { }

    public virtual void HandleCursorMoveLeft() { }

    public virtual void HandleCursorMoveRight() { }

    public virtual PauseTab MoveTabHighlight(
        VisualElement highlight,
        bool isNext
    )
    {
        var target = isNext ? NextTab : PreviousTab;
        highlight.SetTargetPosition(target.TabElement);
        return target;
    }

    public abstract void HandleCursorCancel();

    public abstract void HandleCursorSelect();

    protected virtual void OpenScreen()
    {
        _Screen = _screenAsset.CloneTree();
        Root.Add(_Screen);
        _IsOpen = true;
    }

    protected virtual void CloseScreen()
    {
        if (_Screen != null)
        {
            Root.Remove(_Screen);
            _IsOpen = false;
            _Screen = null;
            return;
        }

        OnTabCancel.Invoke();
    }
}
