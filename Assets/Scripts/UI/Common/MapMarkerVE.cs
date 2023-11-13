using UnityEngine.UIElements;

public class MapMarkerVE : VisualElement
{
    // public MapMarkerVE(
    //     TemplateContainer template, MapMarker marker, Camera camera
    // )
    // {
    //     var icon = template.GetElement("v_image_map-marker");
    //     icon.SetImage(marker.Icon);
    //     Template = template;
    //     _marker = marker;
    //     _mapCamera = camera;
    //     _t = Template.transform;
    //     UpdateMarkerPosition();
    // }
    //
    internal readonly TemplateContainer Template;
    // private readonly MapMarker _marker;
    // private readonly Camera _mapCamera;
    // private readonly bool _shouldRotate;
    // private readonly ITransform _t;
    //
    internal void UpdateMarkerPosition()
    {
        // Template.SetWorldToCameraSpacePosition(
        //     _marker.Position, _mapCamera, false
        // );
    }
}
