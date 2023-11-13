using UnityEngine;

[CreateAssetMenu(fileName = "Prop_NAME", menuName = "Data/Prop data")]
public class PropDataSO : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    [field: TextArea(3, 3)]
    public string Description { get; private set; }

    [field: SerializeField]
    public PropGroup Group { get; private set; }

    [field: SerializeField]
    public SoundEffectAudioDataSO SoundEffect { get; private set; }

    [field: Header("Size info")]
    [field: Tooltip("Size in cm")]
    [field: SerializeField]
    public float Size { get; private set; }

    [field: SerializeField]
    public PropSizeCategory SizeCategory { get; private set; }
}
