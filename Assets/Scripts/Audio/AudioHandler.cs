using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
    private AudioSource _audioSource;

    private AudioClip _currentClip;
    private VaultKey _key;
    private MusicAudioDataSO _music;
    private SoundEffectAudioDataSO _soundEffect;
    private bool _useUnscaledTime;

    private void Awake() => TryGetComponent(out _audioSource);

    private void Reset(AudioSource source = null)
    {
        source ??= _audioSource;
        StopAllCoroutines();
        source.DOKill();
        source.Stop();
        source.clip = null;
        source.pitch = 1f;
        source.time = 0f;
        source.volume = 1f;
        source.loop = false;

        _currentClip = null;
        _soundEffect = null;
        _music = null;
        _useUnscaledTime = false;

        OnEmitterFinished?.Invoke(this, _key);
        _key = VaultKey.Invalid;
    }
    public event UnityAction<AudioHandler, VaultKey> OnEmitterFinished;

    public void BeginPlayback(AudioDataBase cue, VaultKey key)
    {
        _key = key;
        if (cue.Type == AudioType.Music)
        {
            _music = cue as MusicAudioDataSO;
            PlayMusic(_music);
        }
        else if (cue.Type == AudioType.SoundEffect)
        {
            _soundEffect = cue as SoundEffectAudioDataSO;
            _useUnscaledTime = _soundEffect.IsMenuSound;
            ParseSoundEffectSetting(_soundEffect);
        }
    }

    /// <summary>
    ///     Fades the audio out for the remaining duration
    /// </summary>
    public void Fade() => Fade(_currentClip.length - _audioSource.time);

    /// <summary>
    ///     Fades the audio out over the specified duration
    /// </summary>
    /// <param name="fadeDuration"></param>
    public void Fade(float fadeDuration)
    {
        if (_soundEffect?.Setting == AudioPlaybackSetting.AllAtOnce)
        {
            foreach (var source in GetComponents<AudioSource>())
            {
                source.DOFade(0, fadeDuration);
            }
        }

        _audioSource.DOFade(0, fadeDuration).OnComplete(() => Reset());
    }

    /// <summary>
    ///     Pauses the music clip
    /// </summary>
    /// <param name="shouldPause">
    ///     Whether or not to pause the music
    /// </param>
    public void PauseUnpause(bool shouldPause)
    {
        foreach (var source in GetComponents<AudioSource>())
        {
            if (shouldPause)
            {
                source.Pause();
                continue;
            }
            source.UnPause();
        }
    }

    public void RampPitch(float startPitch, float targetPitch, float duration)
    {
        if (_soundEffect?.Setting == AudioPlaybackSetting.AllAtOnce)
        {
            var sources = GetComponents<AudioSource>().ToList();
            sources.ForEach(
                source => {
                    source.DOKill();
                    source.pitch = startPitch;
                    source.DOPitch(targetPitch, duration);
                }
            );
            return;
        }
        _audioSource.DOKill();
        _audioSource.pitch = startPitch;
        _audioSource.DOPitch(targetPitch, duration);
    }

    public void RampPitch(float target, float duration)
    {
        if (_soundEffect?.Setting == AudioPlaybackSetting.AllAtOnce)
        {
            var sources = GetComponents<AudioSource>().ToList();
            sources.ForEach(
                source => {
                    source.DOKill();
                    source.DOPitch(target, duration);
                }
            );
        }
        else
        {
            _audioSource.DOKill();
            _audioSource.DOPitch(target, duration);
        }
    }

    public List<float> RampVolume(
        float startVolume,
        float targetVolume,
        float duration
    )
    {
        List<float> currentVolumes = new();
        if (_soundEffect?.Setting == AudioPlaybackSetting.AllAtOnce)
        {
            List<AudioSource> sources = new(GetComponents<AudioSource>());
            sources.ForEach(
                source => {
                    currentVolumes.Add(source.volume);
                    source.volume = startVolume;
                    source.DOFade(targetVolume, duration);
                }
            );
        }
        else
        {
            currentVolumes.Add(_audioSource.volume);
            _audioSource.volume = startVolume;
            _audioSource.DOFade(targetVolume, duration);
        }
        return currentVolumes;
    }

    public void SetPriority(int priority) => _audioSource.priority = priority;

    /// <summary>
    ///     Stops the audio immediately
    /// </summary>
    public void Stop()
    {
        switch (_soundEffect.Setting)
        {
            case AudioPlaybackSetting.AllAtOnce:
                List<AudioSource> sources = new(GetComponents<AudioSource>());
                // NOTE starts at 1 to avoid grabbing the default source
                for (var i = 1; i < sources.Count; i++)
                {
                    Destroy(sources[i]);
                }
                break;

            case AudioPlaybackSetting.StartLoopStop:
                StopAllCoroutines();
                _audioSource.Stop();
                _audioSource.loop = false;
                SetClip(_soundEffect.SoundFX.Last());
                _audioSource.Play();
                WrapItUp();
                return;
        }
        Reset();
    }

    /// <summary>
    ///     Plays a random sound effect clip from an audio cue list
    /// </summary>
    /// <param name="sfx">
    ///     The sound effect cue
    /// </param>
    private void ParseSoundEffectSetting(SoundEffectAudioDataSO sfx)
    {
        if (sfx.SoundFX.Count == 0)
        {
            Debug.LogWarning($"{sfx.name} has no sounds to play.");
            return;
        }
        SetNewPitch(sfx.PitchVariation);
        Set3dSound(sfx.Is3DSound);

        switch (sfx.Setting)
        {
            case AudioPlaybackSetting.OneRandom:
                PlayRandom(sfx);
                break;

            case AudioPlaybackSetting.All:
                StartCoroutine(PlayAllRoutine(sfx));
                break;

            case AudioPlaybackSetting.OneRandomRepeat:
                PlayRandom(sfx, true);
                break;

            case AudioPlaybackSetting.AllRepeat:
                StartCoroutine(PlayAllRoutine(sfx, repeat: true));
                break;

            case AudioPlaybackSetting.AllShuffled:
                StartCoroutine(PlayAllRoutine(sfx, true));
                break;

            case AudioPlaybackSetting.AllAndFade:
                StartCoroutine(PlayAllRoutine(sfx, fade: true));
                break;

            case AudioPlaybackSetting.OneRandomAndFade:
                PlayRandom(sfx, fade: true);
                break;

            case AudioPlaybackSetting.AllShuffleAndFade:
                StartCoroutine(PlayAllRoutine(sfx, true, fade: true));
                break;

            case AudioPlaybackSetting.AllShuffleRepeat:
                StartCoroutine(
                    PlayAllRoutine(sfx, true, true)
                );
                break;

            case AudioPlaybackSetting.AllAtOnce:
                StartCoroutine(PlayAllAtOnceRoutine(sfx));
                break;

            case AudioPlaybackSetting.CarStartup:
                StartCoroutine(PlayCarStartupRoutine(sfx));
                break;

            case AudioPlaybackSetting.StartLoopStop:
                StartCoroutine(PlayCarStartupRoutine(sfx));
                break;

            default:
                var exception = $"{sfx.name}: Something's not right.";
                throw new(exception);
        }
    }

    /// <summary>
    ///     Sets up the emitter
    /// </summary>
    /// <param name="clip">
    ///     The clip to play
    /// </param>
    private void SetClip(AudioClip clip) => SetClip(_audioSource, clip);

    /// <summary>
    ///     Overload for sounds with
    ///     <c>
    ///         AudioPlaybackSetting.AllAtOnce
    ///     </c>
    ///     .
    /// </summary>
    /// <param name="source"></param>
    /// <param name="clip"></param>
    private void SetClip(AudioSource source, AudioClip clip)
    {
        name = clip.name.GetNameTag();
        _currentClip = clip;
        source.clip = clip;
    }

    private void Set3dSound(bool is3d) => Set3dSound(_audioSource, is3d);

    private void Set3dSound(AudioSource source, bool is3d) =>
        source.spatialBlend = is3d ? 1f : 0f;

    /// <summary>
    ///     Sets a new pitch for the audio source.
    ///     If > 0 will waver from 1 to that amount.</param>
    /// </summary>
    /// <param name="variation">
    private void SetNewPitch(float variation) =>
        SetNewPitch(_audioSource, variation);

    private void SetNewPitch(AudioSource source, float variation) =>
        // TODO Use static Util.
        source.pitch = Random.Range(1f - variation, 1f + variation);

    // CLEANUP move all the PlayAllAtOnce stuff into a separate audio emitter
    private IEnumerator PlayAllAtOnceRoutine(SoundEffectAudioDataSO sfx)
    {
        name = sfx.name.GetNameTag();
        List<AudioSource> tempSourcesList = new();
        var longest = 0f;
        var listPriority = _audioSource.priority;
        var minDistance = _audioSource.minDistance;
        var maxDistance = _audioSource.maxDistance;
        var rolloffMode = _audioSource.rolloffMode;
        sfx.SoundFX.ForEach(
            clip => {
                var source = gameObject.AddComponent<AudioSource>();
                longest = clip.length > longest ? clip.length : longest;
                source.clip = clip;
                SetNewPitch(source, sfx.PitchVariation);
                Set3dSound(source, sfx.Is3DSound);
                source.priority = listPriority;
                source.minDistance = minDistance;
                source.maxDistance = maxDistance;
                source.rolloffMode = rolloffMode;
                tempSourcesList.Add(source);
                listPriority += sfx.Priority ? 1 : -1;
                source.Play();
            }
        );

        yield return new WaitForSeconds(longest);
        tempSourcesList.ForEach(s => Destroy(s));
        Reset();
    }

    private IEnumerator PlayAllRoutine(
        SoundEffectAudioDataSO sfx,
        bool shuffle = false,
        bool repeat = false,
        bool fade = false
    )
    {
        if (fade)
        {
            float totalLength = 0;
            sfx.SoundFX.ForEach(clip => totalLength += clip.length);
            Fade(totalLength);
        }

        foreach (var clip in shuffle ? sfx.SoundFX.Randomize() : sfx.SoundFX)
        {
            SetClip(clip);
            _audioSource.Play();
            yield return new WaitForSeconds(clip.length);

            if (sfx.VaryPitchForEach)
            {
                SetNewPitch(sfx.PitchVariation);
            }
        }

        if (repeat)
        {
            StartCoroutine(PlayAllRoutine(sfx, shuffle, repeat, fade));
            yield break;
        }

        WrapItUp();
    }

    private IEnumerator PlayCarStartupRoutine(SoundEffectAudioDataSO sfx)
    {
        SetClip(sfx.SoundFX[0]);
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);

        _audioSource.loop = true;
        SetClip(sfx.SoundFX[1]);
        _audioSource.Play();
    }

    /// <summary>
    ///     Plays a music clip from an audio cue
    /// </summary>
    /// <param name="music"></param>
    private void PlayMusic(MusicAudioDataSO music)
    {
        var newClip = music.Music;
        _audioSource.loop = true;

        // Restart the music only if a different track is requested
        if (_currentClip != newClip)
        {
            SetClip(newClip);
            _audioSource.Play();
        }
    }

    private void PlayRandom(
        SoundEffectAudioDataSO sfx,
        bool repeat = false,
        bool fade = false
    )
    {
        SetClip(sfx.SoundFX.Random());
        _audioSource.loop = repeat;
        _audioSource.Play();
        if (fade)
        {
            Fade();
            return;
        }
        if (!repeat)
        {
            WrapItUp();
        }
    }

    /// <summary>
    ///     Waits for the clip to finish playing, then resets the emitter object
    /// </summary>
    /// <returns></returns>
    private void WrapItUp() => StartCoroutine(WrapItUp(_currentClip.length));

    private IEnumerator WrapItUp(float duration)
    {
        yield return _useUnscaledTime
            ? new WaitForSecondsRealtime(duration)
            : new WaitForSeconds(duration);
        Reset();
    }
}
