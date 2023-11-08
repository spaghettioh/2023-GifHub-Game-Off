using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Sound effect", fileName = "SFX_NAME")]
public class SoundEffectAudioDataSO : AudioDataBase
{
    /// <summary>
    /// The list of clips in the asset
    /// </summary>
    [field: SerializeField]
    public List<AudioClip> SoundFX { get; private set; }

    /// <summary>
    /// The AudioSetting for playback in the asset
    /// </summary>
    [field: SerializeField]
    public AudioPlaybackSetting Setting { get; private set; }

    /// <summary>
    /// The amount of randomness to vary playback of the sound
    /// </summary>
    [Tooltip("Variation in pitch for each playback.")]
    [field: SerializeField, Range(0, 1)]
    public float PitchVariation { get; private set; }

    [field: SerializeField]
    public bool VaryPitchForEach { get; private set; }

    [Tooltip(
        "Menu sounds play in realtime so that when the game is paused the sound will still wrap appropriately."
    )]
    [field: SerializeField]
    public bool IsMenuSound { get; private set; }

    [Tooltip(
        "3D sounds are meant to be played in the world, and will track their target to update their position."
    )]
    [field: SerializeField]
    public bool Is3DSound { get; private set; }

    private void OnValidate()
    {
        if (Setting == AudioPlaybackSetting.CarStartup && SoundFX.Count != 2)
        {
            throw new Exception(
                $"{name} : {Setting} must have exactly 2 clips."
            );
        }
        if (Setting == AudioPlaybackSetting.StartLoopStop && SoundFX.Count != 3)
        {
            throw new Exception(
                $"{name} : {Setting} must have exactly 3 clips."
            );
        }
    }
}
