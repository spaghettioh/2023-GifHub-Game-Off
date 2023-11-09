using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MiniMapHUDManager : HUDManager
{
    [SerializeField]
    private string _markerElementId;

    [SerializeField]
    private ClumpDataSO _clumpRuntimeData;

    private TemplateContainer _template;

    private void Start()
    {
        _template = _HUD._Root.GetTemplate(_markerElementId);
    }

    private void FixedUpdate()
    {
        if (_clumpRuntimeData.Transform.IsNull())
        {
            return;
        }
        var rotation = _clumpRuntimeData.Euler.y.Round(10);
        _template.transform.rotation = Quaternion.Euler(new(0f, 0f, rotation));
    }
}
