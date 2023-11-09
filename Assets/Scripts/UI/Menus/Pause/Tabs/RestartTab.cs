using System;
using UnityEngine;

[Serializable]
public class RestartTab : PauseTab
{
    public event Action OnRestartRequested;

    [SerializeField]
    private string _headingId;

    [SerializeField]
    private string _headingText;

    [SerializeField]
    private string _bodyId;

    [SerializeField]
    private string _bodyText;

    [SerializeField]
    internal string _SceneName;

    public override void HandleCursorCancel()
    {
        if (_IsOpen)
        {
            CloseScreen();
            return;
        }
        RaiseCancelEvent();
    }

    public override void HandleCursorSelect()
    {
        if (_IsOpen)
        {
            CloseScreen();
            OnRestartRequested?.Invoke();
            return;
        }

        OpenScreen();
    }

    protected override void OpenScreen()
    {
        base.OpenScreen();
        var label = _Screen.GetLabel(_headingId);
        label.SetText(_headingText);
        label = _Screen.GetLabel(_bodyId);
        label.SetText(_bodyText);
    }
}
