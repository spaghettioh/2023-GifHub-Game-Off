using UnityEngine;

[CreateAssetMenu(
    menuName = "Audio/Audio Emitter Pool", fileName = "AudioEmitterPool"
)]
public class AudioHandlerPoolSO : ComponentPool<AudioHandler>
{
    private readonly Vector2Int _highRange = new(0, 10);
    private int _currentHighPriority = 10;

    private readonly Vector2Int _lowRange = new(11, 256);
    private int _currentLowPriority = 256;

    public AudioHandler Request(AudioDataBase cue)
    {
        var emitter = base.Request();
        if (cue.Priority)
        {
            emitter.SetPriority(_currentHighPriority--);
        }
        else
        {
            emitter.SetPriority(_currentLowPriority--);
        }
        return emitter;
    }

    protected override void OnRequest(AudioHandler handler)
    {
        ResetPriorityCheck(ref _currentLowPriority, _lowRange);
        ResetPriorityCheck(ref _currentHighPriority, _highRange);
        base.OnRequest(handler);
    }

    private void ResetPriorityCheck(ref int priority, Vector2Int range) =>
        priority = priority <= range.x ? range.y : priority;
}
