using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
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
            return;

        OpenScreen();
    }
}
