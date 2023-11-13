using System;
using UnityEngine;

[Serializable]
public class UIPresence
{
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    [field: TextArea(1, 6)]
    public string Description { get; private set; }

    [field: SerializeField]
    public Sprite Icon { get; private set; }
}
