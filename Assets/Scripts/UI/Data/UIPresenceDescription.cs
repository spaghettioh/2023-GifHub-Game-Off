using System;
using UnityEngine;

[Serializable]
public class UIPresenceDescription
{
    [TextArea(1, 3)]
    [SerializeField]
    private string _text;

    public string Text => "";
}
