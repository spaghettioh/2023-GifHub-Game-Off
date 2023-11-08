// using UnityEngine;
//
// public class SpawnManager : MonoBehaviour
// {
//     [Header("Listening to..."), SerializeField]
//     private IntEventSO _stageGenerated;
//
//     private void OnEnable() =>
//         _stageGenerated.OnEventRaised += OnLevelGenerated;
//
//     private void OnDisable() =>
//         _stageGenerated.OnEventRaised -= OnLevelGenerated;
//
//     /// <summary>
//     ///     Spawns an object prefab when the event is fired.
//     /// </summary>
//     private void OnLevelGenerated(int stageGearLevel)
//     {
//         GetComponent<AtlasSpawner>().Spawn();
//     }
// }
