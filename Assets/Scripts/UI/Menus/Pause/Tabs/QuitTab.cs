using System;
using UnityEngine;

[Serializable]
public class QuitTab : PauseTab
{
    [SerializeField]
    private string _headingId;

    [SerializeField]
    private string _headingText;

    [SerializeField]
    private string _bodyId;

    [SerializeField]
    private string _bodyText;

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
            Debug.Log("Wants to quit");
            Application.Quit();
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
