using UnityEngine;

[CreateAssetMenu(fileName = "UI_MenuSounds", menuName = "UI/Menu sounds")]
public class MenuSoundsSO : ScriptableObject
{
    [field: SerializeField]
    public SoundEffectAudioDataSO Open { get; private set; }

    [field: SerializeField]
    public SoundEffectAudioDataSO Move { get; private set; }

    [field: SerializeField]
    public SoundEffectAudioDataSO Select { get; private set; }

    [field: SerializeField]
    public SoundEffectAudioDataSO Cancel { get; private set; }

    [field: SerializeField]
    public SoundEffectAudioDataSO Invalid { get; private set; }
}
