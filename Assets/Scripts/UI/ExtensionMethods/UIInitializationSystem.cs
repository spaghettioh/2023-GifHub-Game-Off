using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UIInitializationSystem : MonoBehaviour
{
    [SerializeField]
    private PanelSettings _panelSettings;

    [FormerlySerializedAs("_clumpTransform")]
    [SerializeField]
    private TransformEventSO _transformEvent;

    private void OnEnable() =>
        _transformEvent.OnEventRaised += HandleClumpSpawned;

    private void OnDisable() =>
        _transformEvent.OnEventRaised -= HandleClumpSpawned;
    
    private void Start() => _panelSettings.SetUISize();

    private static void HandleClumpSpawned(Transform _) =>
        Camera.main.SetUICamera();
}
