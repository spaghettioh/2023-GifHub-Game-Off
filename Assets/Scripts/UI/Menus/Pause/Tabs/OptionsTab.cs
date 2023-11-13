using System;

[Serializable]
public class OptionsTab : PauseTab
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

    public override void HandleCursorSelect()
    {
        if (_IsOpen)
        {
            return;
        }

        OpenScreen();
    }
}
