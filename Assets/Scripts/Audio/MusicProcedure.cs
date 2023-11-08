// using UnityEngine;
//
// public class MusicProcedure : MonoBehaviour
// {
//     [SerializeField]
//     private SceneReferenceSO _thisScene;
//
//     [Header("Listening to...")]
//     [SerializeField]
//     private IntEventSO _startMusic;
//
//     [Header("Broadcasting to...")]
//     [SerializeField]
//     private AudioEventSO _audioEvent;
//
//     private void OnEnable() => _startMusic.OnEventRaised += OnStartMusic;
//
//     private void OnDisable() => _startMusic.OnEventRaised -= OnStartMusic;
//
//     private void OnStartMusic(int _)
//     {
//         var music = _thisScene.Music;
//         _audioEvent.RaisePlayback(music, name);
//     }
// }
