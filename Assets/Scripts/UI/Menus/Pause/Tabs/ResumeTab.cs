using System;

[Serializable]
public class ResumeTab : PauseTab
{
    public override void HandleCursorCancel()
    {
        if (_IsOpen)
        {
            CloseScreen();
            return;
        }
        RaiseCancelEvent();
    }

    public override void HandleCursorSelect() => CloseScreen();
}
