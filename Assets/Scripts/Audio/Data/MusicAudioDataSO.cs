using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Music", fileName = "Music_NAME")]
public class MusicAudioDataSO : AudioDataBase
{
    [field: SerializeField]
    public AudioClip Music { get; private set; }
}
