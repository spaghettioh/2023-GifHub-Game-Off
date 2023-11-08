using UnityEngine;

public abstract class AudioDataBase : ScriptableObject
{
    /// <summary>
    /// The type of audio stored in this cue.
    /// </summary>
    [field: SerializeField]
    public AudioType Type { get; private set; }

    /// <summary>
    /// The priority of the audio stored when it's played.
    /// </summary>
    [SerializeField]
    private bool _priority;
    public bool Priority => _priority;

    private void OnValidate()
    {
        if (Type == AudioType.Music && !_priority)
        {
            Debug.LogWarning($"{name}: Music is always high priority!");
            _priority = true;
        }
    }
}
