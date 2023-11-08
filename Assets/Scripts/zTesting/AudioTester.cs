// using UnityEngine;
//
// public class AudioTester : MonoBehaviour
// {
//     public AudioEventSO AudioEvent;
//     public SoundEffectAudioDataSO Sound;
//     public MusicAudioDataSO Music;
//
//     private VaultKey _soundKey;
//
//     [ContextMenu("Play sound")]
//     private void PlaySound() =>
//         _soundKey = AudioEvent.RaisePlayback(Sound, name);
//
//     [ContextMenu("Play music")]
//     private void PlayMusic() => AudioEvent.RaisePlayback(Music, name);
//
//     [ContextMenu("Fade music")]
//     private void FadeMusic() => AudioEvent.RaiseFadeMusic(3, name);
//
//     [ContextMenu("Stop music")]
//     private void StopMusic() => AudioEvent.RaiseStopMusic(name);
//
//     [ContextMenu("Pause music")]
//     private void PauseMusic() => AudioEvent.RaisePauseUnpauseMusic(true, name);
//
//     [ContextMenu("Unpause music")]
//     private void UnpauseMusic() =>
//         AudioEvent.RaisePauseUnpauseMusic(false, name);
//
//     [ContextMenu("Ramp sound pitch")]
//     private void RampSoundPitch() =>
//         AudioEvent.RaiseRampPitch(_soundKey, 0, 3, 3, name);
//
//     [ContextMenu("Ramp sound vol")]
//     private void RampSoundVol() =>
//         AudioEvent.RaiseRampVolume(_soundKey, 1, 0, 3, name);
//
//     [ContextMenu("Stop sound")]
//     private void StopSound() => AudioEvent.RaiseStopPlayback(_soundKey, name);
// }
