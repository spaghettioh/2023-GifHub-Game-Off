// using System;
// using UnityEngine;
//
// [CreateAssetMenu(fileName = "Audio_Event", menuName = "Audio/Audio Event")]
// public class AudioEventSO : ScriptableObject
// {
//     public event VaultKeyAction<AudioDataBase, Vector3> OnPlayback;
//     public event Action<float> OnMusicFadeRequested;
//     public event Action OnStopMusicRequested;
//     public event Action<bool> OnPauseUnpauseMusicRequested;
//     public event Action<VaultKey, bool> OnPauseUnpause;
//     public event Action<VaultKey, float, float, float> OnRampPitchWithStart;
//
//     public event Action<VaultKey, float, float> OnRampPitch;
//     public event Action<VaultKey, float, float, float> OnRampVolume;
//     public event Action<VaultKey> OnStopPlayback;
//     public event Action<bool> OnOpenCloseMenu;
//
//     public void RaiseOpenMenu(bool isOpen, string src) =>
//         OnOpenCloseMenu.CheckSubscriptions(isOpen, src.BuildLogMessage(name));
//
//     public VaultKey RaisePlayback(AudioDataBase audioCue) =>
//         RaisePlayback(audioCue, default, "A UnityEvent (probably)");
//
//     public VaultKey RaisePlayback(AudioDataBase audioCue, string src) =>
//         RaisePlayback(audioCue, default, src.BuildLogMessage(name));
//
//     public VaultKey RaisePlayback(
//         AudioDataBase audioCue, Vector3 position, string src
//     ) =>
//         OnPlayback.CheckKeySubscriptions(
//             audioCue, position, src.BuildLogMessage(name)
//         );
//
//     public void PauseUnpause(VaultKey key, bool shouldPause, string src) =>
//         OnPauseUnpause.CheckSubscriptions(
//             key, shouldPause, src.BuildLogMessage(name)
//         );
//
//     public void RaiseStopPlayback(VaultKey key, string src) =>
//         OnStopPlayback.CheckSubscriptions(key, src.BuildLogMessage(name));
//
//     public void RaiseRampPitch(
//         VaultKey key, float startPitch, float targetPitch, float duration,
//         string src
//     ) =>
//         OnRampPitchWithStart.CheckSubscriptions(
//             key, startPitch, targetPitch, duration, src.BuildLogMessage(name)
//         );
//
//     public void RaiseRampPitch(
//         VaultKey key, float targetPitch, float duration, string src
//     ) =>
//         OnRampPitch.CheckSubscriptions(
//             key, targetPitch, duration, src.BuildLogMessage(name)
//         );
//
//     public void RaiseRampVolume(
//         VaultKey key, float startVolume, float targetVolume, float duration,
//         string src
//     ) =>
//         OnRampVolume.CheckSubscriptions(
//             key, startVolume, targetVolume, duration, src.BuildLogMessage(name)
//         );
//
//     public void RaiseFadeMusic(float fadeLength) =>
//         RaiseFadeMusic(fadeLength, "A UnityEvent, probably,");
//
//     public void RaiseFadeMusic(float fadeLength, string src) =>
//         OnMusicFadeRequested.CheckSubscriptions(
//             fadeLength, src.BuildLogMessage(name)
//         );
//
//     public void RaiseStopMusic(string src) =>
//         RaiseFadeMusic(0f, src.BuildLogMessage(name));
//
//     public void RaisePauseUnpauseMusic(bool pauseUnpause, string src) =>
//         OnPauseUnpauseMusicRequested.CheckSubscriptions(
//             pauseUnpause, src.BuildLogMessage(name)
//         );
// }
