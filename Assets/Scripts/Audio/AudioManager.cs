// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// public class AudioManager : MonoBehaviour
// {
//     [FormerlySerializedAs("_audioEmitterPool")]
//     [SerializeField]
//     private AudioHandlerPoolSO _audioHandlerPool;
//
//     [Header("Listening to...")]
//     [SerializeField]
//     private AudioEventSO _audioEvent;
//
//     private AudioHandler _musicHandler;
//     private AudioHandler _fadingMusicHandler;
//     private bool _hasMusicEmitter => _musicHandler != null;
//     private readonly Vault<AudioDataBase, AudioHandler> _vault = new();
//     private List<float> _volumesForMenu = new();
//
//     private void Awake()
//     {
//         _audioHandlerPool.PreWarm(transform);
//     }
//
//     private void OnEnable()
//     {
//         _audioEvent.OnPlayback += HandlePlayback;
//         _audioEvent.OnPauseUnpause += HandlePauseUnpause;
//         _audioEvent.OnStopPlayback += HandleStopPlayback;
//         _audioEvent.OnRampPitch += HandleRampPitch;
//         _audioEvent.OnRampPitchWithStart += HandleRampPitchWithStart;
//         _audioEvent.OnRampVolume += HandleRampVolume;
//         _audioEvent.OnMusicFadeRequested += HandleFadeOutMusic;
//         _audioEvent.OnStopMusicRequested += HandleStopMusic;
//         _audioEvent.OnPauseUnpauseMusicRequested += HandlePauseUnpauseMusic;
//         _audioEvent.OnOpenCloseMenu += HandleOpenCloseMenu;
//     }
//
//     private void OnDisable()
//     {
//         _audioEvent.OnPlayback -= HandlePlayback;
//         _audioEvent.OnPauseUnpause -= HandlePauseUnpause;
//         _audioEvent.OnStopPlayback -= HandleStopPlayback;
//         _audioEvent.OnRampPitch -= HandleRampPitch;
//         _audioEvent.OnRampPitchWithStart -= HandleRampPitchWithStart;
//         _audioEvent.OnRampVolume -= HandleRampVolume;
//         _audioEvent.OnMusicFadeRequested -= HandleFadeOutMusic;
//         _audioEvent.OnStopMusicRequested -= HandleStopMusic;
//         _audioEvent.OnPauseUnpauseMusicRequested -= HandlePauseUnpauseMusic;
//         _audioEvent.OnOpenCloseMenu -= HandleOpenCloseMenu;
//     }
//
//     private void HandleOpenCloseMenu(bool isOpen) =>
//         _vault.Entities.ForEach(emitter => emitter.PauseUnpause(isOpen));
//
//     private VaultKey HandlePlayback(AudioDataBase audioCue, Vector3 position)
//     {
//         AudioHandler handler = _audioHandlerPool.Request(audioCue);
//         handler.gameObject.SetActive(true);
//         handler.OnEmitterFinished += ReturnEmitterToPool;
//         var key = VaultKey.Invalid;
//
//         if (audioCue is MusicAudioDataSO)
//         {
//             SetUpMusicEmitter(handler);
//         }
//         else
//         {
//             SetUpSFXEmitter(handler, position);
//             key = _vault.Add(audioCue, handler);
//         }
//         handler.BeginPlayback(audioCue, key);
//         return key;
//     }
//
//     private void HandlePauseUnpause(VaultKey key, bool shouldPause)
//     {
//         if (_vault.TryGetEntity(key, out var emitter))
//         {
//             emitter.PauseUnpause(shouldPause);
//         }
//     }
//
//     private void HandleRampVolume(
//         VaultKey key,
//         float startVolume,
//         float targetVolume,
//         float duration
//     )
//     {
//         if (_vault.TryGetEntity(key, out var emitter))
//         {
//             emitter.RampVolume(startVolume, targetVolume, duration);
//         }
//     }
//
//     private void HandleRampPitch(VaultKey key, float target, float duration)
//     {
//         if (_vault.TryGetEntity(key, out var emitter))
//         {
//             emitter.RampPitch(target, duration);
//         }
//     }
//
//     private void HandleRampPitchWithStart(
//         VaultKey key,
//         float start,
//         float target,
//         float duration
//     )
//     {
//         if (_vault.TryGetEntity(key, out var emitter))
//         {
//             emitter.RampPitch(start, target, duration);
//         }
//     }
//
//     private void HandleStopPlayback(VaultKey key)
//     {
//         if (!_vault.Keys.Contains(key))
//         {
//             return;
//         }
//         if (_vault.TryGetEntity(key, out var emitter))
//         {
//             emitter.Stop();
//         }
//     }
//
//     private void SetUpSFXEmitter(AudioHandler handler, Vector3 position)
//     {
//         if (position != default)
//         {
//             handler.transform.position = position;
//         }
//     }
//
//     private void SetUpMusicEmitter(AudioHandler handler)
//     {
//         // Create a music emitter if one doesn't exist
//         if (!_hasMusicEmitter)
//         {
//             _musicHandler = handler;
//         }
//     }
//
//     private void HandleFadeOutMusic(float fadeLength)
//     {
//         if (!_hasMusicEmitter)
//         {
// #if UNITY_EDITOR
//             Debug.LogWarning(
//                 $"{name} heard Fade event but there's no music emitter"
//             );
// #endif
//             return;
//         }
//
//         _fadingMusicHandler = _musicHandler;
//         _musicHandler = null;
//         _fadingMusicHandler.Fade(fadeLength);
//     }
//
//     private void HandleStopMusic()
//     {
//         if (!_hasMusicEmitter)
//         {
// #if UNITY_EDITOR
//             Debug.LogWarning(
//                 $"{name} heard Stop event but there's no music emitter"
//             );
// #endif
//             return;
//         }
//
//         _musicHandler.Fade();
//     }
//
//     private void HandlePauseUnpauseMusic(bool pauseUnpause)
//     {
//         if (!_hasMusicEmitter)
//         {
// #if UNITY_EDITOR
//             Debug.LogWarning(
//                 $"{name} heard Pause event but there's " + $"no music emitter"
//             );
// #endif
//             return;
//         }
//
//         _musicHandler.PauseUnpause(pauseUnpause);
//     }
//
//     private void ReturnEmitterToPool(AudioHandler handler, VaultKey key)
//     {
//         handler.OnEmitterFinished -= ReturnEmitterToPool;
//         _vault.Remove(key);
//         _audioHandlerPool.Return(handler);
//     }
// }
