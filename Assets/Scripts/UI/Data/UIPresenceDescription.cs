using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIPresenceDescription
{
    [TextArea(1, 3), SerializeField]
    private string _text;

    public string Text
    {
        get { return ""; }
    }
}
