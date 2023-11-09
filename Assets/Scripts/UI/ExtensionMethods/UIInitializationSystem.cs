using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UIInitializationSystem : MonoBehaviour
{
    [SerializeField]
    private PanelSettings _panelSettings;

    [FormerlySerializedAs("_atlasTransform")]
    [SerializeField]
    private TransformAnchorSO _transformAnchor;

    private void OnEnable() =>
        _transformAnchor.OnTransformSet += HandleSpawned;

    private void OnDisable() =>
        _transformAnchor.OnTransformSet -= HandleSpawned;
    
    private void Start() => _panelSettings.SetUISize();

    private static void HandleSpawned(Transform _) =>
        Camera.main.SetUICamera();
}
