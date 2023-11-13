/// <summary>
///     Used to determine how the cue will be configured for playback
/// </summary>
public enum AudioType
{
    SoundEffect,
    Music,
}

/// <summary>
///     Used to determing if the cue can be pushed aside for another one.
/// </summary>
public enum AudioPriority
{
    Low,
    High,
}

/// <summary>
///     The playback setting for an AudioSO.
/// </summary>
public enum AudioPlaybackSetting
{
    /// <summary>
    ///     Plays a random clip from the list.
    /// </summary>
    OneRandom,

    /// <summary>
    ///     Plays a random clip forever.
    /// </summary>
    OneRandomRepeat,

    /// <summary>
    ///     Plays a random clip immediately fading to 0 in volume.
    /// </summary>
    OneRandomAndFade,

    /// <summary>
    ///     Plays all clips in order.
    /// </summary>
    All,

    /// <summary>
    ///     Plays all clips in random order.
    /// </summary>
    AllShuffled,

    /// <summary>
    ///     Plays all clips in order forever.
    /// </summary>
    AllRepeat,

    /// <summary>
    ///     Plays all clips in order and fades the volume to 0 by the
    ///     end of the last clip.
    /// </summary>
    AllAndFade,

    /// <summary>
    ///     Plays all clips in random order forever.
    /// </summary>
    AllShuffleRepeat,

    /// <summary>
    ///     Plays all clips in random order and fades the volume to 0 by the
    ///     end of the last clip.
    /// </summary>
    AllShuffleAndFade,

    /// <summary>
    ///     Plays every clip once at the same time.
    /// </summary>
    AllAtOnce,

    /// <summary>
    ///     Plays the clips in order, then repeats the last clip.
    /// </summary>
    CarStartup,

    /// <summary>
    ///     Plays the first clip, then loops the second. Will play the last clip
    ///     when told to stop.
    /// </summary>
    StartLoopStop,
}
