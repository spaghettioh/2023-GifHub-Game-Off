using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapMenu : MenuUI
{
    [SerializeField]
    private string _mapContainerId;

    [SerializeField]
    private UITemplatePool _mapMarkerTemplatePool;

    [SerializeField]
    private TransformAnchorSO _mapMenuCameraTransform;

    private Camera _mapMenuCamera;
    private VisualElement _mapContainer;
    private List<MapMarkerVE> _mapMarkerVEs = new();
    private bool _isOpen;

    [SerializeField]
    private string _mapName;

    protected override void OnMenuEnable()
    {
        _Input.Clump.OnOpenMapInput += HandleOpenMap;
        _Input.Map.OnCloseMapInput += HandleCloseMap;
    }

    protected override void OnMenuDisable()
    {
        _Input.Clump.OnOpenMapInput -= HandleOpenMap;
        _Input.Map.OnCloseMapInput -= HandleCloseMap;
    }

    protected override void OnUIStart()
    {
        _mapMenuCameraTransform.Transform.TryGetComponent(out _mapMenuCamera);
        _mapContainer = _Root.GetElement(_mapContainerId);
        _mapMarkerTemplatePool.PreWarm(_mapContainer);
    }

    private void HandleOpenMap()
    {
        // MapMarker[] markers = FindObjectsOfType<MapMarker>();
        // foreach (var mapMarker in markers)
        // {
        //     var template = GetMarker();
        //     var markerVE = new MapMarkerVE(template, mapMarker, _mapMenuCamera);
        //     _mapMarkerVEs.Add(markerVE);
        // }
        OpenMenu();
        _isOpen = true;
        _Input.EnableMap();
    }

    private void HandleCloseMap()
    {
        PlaySound(_MenuSounds.Cancel);
        CloseMenu();
    }

    protected override void HandleCursorCancel() => HandleCloseMap();
    protected override void ResetMenu()
    {
        _mapMarkerVEs.ForEach(
            element => _mapMarkerTemplatePool.Return(element.Template)
        );
        _mapMarkerVEs = new();
        _isOpen = false;
    }

    private TemplateContainer GetMarker() => _mapMarkerTemplatePool.Request();

    private void Update()
    {
        if (!_isOpen)
        {
            return;
        }
        _mapMarkerVEs.ForEach(element => element.UpdateMarkerPosition());
    }
}
