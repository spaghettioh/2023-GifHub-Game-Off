using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
///     UI utilities and method extensions
/// </summary>
public static class ScreenPositionUIExtensions
{
    public static Vector2 UiResolution;
    public static Camera MainCamera;

    public static void SetUISize(this PanelSettings settings) =>
        UiResolution = settings.referenceResolution;

    public static void SetUICamera(this Camera camera) => MainCamera = camera;

    public static Vector3 SetWorldToScreenSpacePosition(
        this VisualElement element, Vector3 worldPosition,
        bool clampToScreenEdge = true
    )
    {
        if (MainCamera.IsNull())
        {
            return Vector3.zero;
        }
        Vector2 size = new(
            element.resolvedStyle.width, element.resolvedStyle.height
        );
        Vector2 screenResolution = new(Screen.width, Screen.height);
        var safeZoneY = screenResolution.y / 3;
        Vector2 minPos = new(size.x / 2, size.y / 2);
        Vector2 maxPos = new(
            screenResolution.x - minPos.x, screenResolution.y - minPos.y
        );
        Vector2 screenPosition = MainCamera.WorldToScreenPoint(worldPosition);

        if (clampToScreenEdge)
        {
            screenPosition.x = Mathf.Clamp(
                screenPosition.x, minPos.x, maxPos.x
            );
            screenPosition.y = Mathf.Clamp(
                screenPosition.y, minPos.y, maxPos.y
            );

            if (screenPosition.x == minPos.x || screenPosition.x == maxPos.x)
            {
                screenPosition.y = Mathf.Clamp(
                    screenPosition.y, safeZoneY + minPos.y,
                    screenResolution.y - safeZoneY - minPos.y
                );
            }
        }

        // Screen height is bottom>up, but UI is top>down
        screenPosition.y = screenResolution.y - screenPosition.y;
        screenPosition.x = screenPosition.x.Remap(
            0, screenResolution.x, 0, UiResolution.x
        );
        screenPosition.y = screenPosition.y.Remap(
            0, screenResolution.y, 0, UiResolution.y
        );

        element.transform.position = screenPosition;
        return screenPosition;
    }

    public static void SetWorldToCameraSpacePosition(
        this VisualElement element, Vector3 worldPosition, Camera camera,
        bool clampToTextureEdge = true
    )
    {
        var parentSize = element.parent.resolvedStyle;
        Vector2 parentResolution = new(parentSize.width, parentSize.height);
        Vector2 cameraPosition = camera.WorldToViewportPoint(worldPosition);

        if (clampToTextureEdge)
        {
            Vector2 elementSize = new(
                element.resolvedStyle.width, element.resolvedStyle.height
            );
            var safeZoneY = parentResolution.y / 3;
            Vector2 minPos = new(elementSize.x / 2, elementSize.y / 2);
            Vector2 maxPos = new(
                parentResolution.x - minPos.x, parentResolution.y - minPos.y
            );
            cameraPosition.x = Mathf.Clamp(
                cameraPosition.x, minPos.x, maxPos.x
            );
            cameraPosition.y = Mathf.Clamp(
                cameraPosition.y, minPos.y, maxPos.y
            );

            if (cameraPosition.x == minPos.x || cameraPosition.x == maxPos.x)
            {
                cameraPosition.y = Mathf.Clamp(
                    cameraPosition.y, safeZoneY + minPos.y,
                    parentResolution.y - safeZoneY - minPos.y
                );
            }
        }

        cameraPosition.x = cameraPosition.x.Remap(0, 1, 0, parentResolution.x);
        cameraPosition.y = cameraPosition.y.Remap(0, 1, 0, parentResolution.y);
        // Screen height is bottom>up, but UI is top>down
        cameraPosition.y = parentResolution.y - cameraPosition.y;

        element.transform.position = cameraPosition;
    }
}
