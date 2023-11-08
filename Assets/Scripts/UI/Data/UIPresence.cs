using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIPresence
{
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField, TextArea(1, 6)]
    public string Description { get; private set; }

    [field: SerializeField]
    public Sprite Icon { get; private set; }
}
