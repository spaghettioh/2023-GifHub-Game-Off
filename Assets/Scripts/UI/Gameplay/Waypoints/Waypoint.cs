using UnityEngine;
using UnityEngine.UIElements;

public class Waypoint
{
    public Waypoint(IInteractable interactable, TemplateContainer template)
    {
        Template = template;
        Interactable = interactable;

        SetWaypointDetails();
        SetWaypointVisible(true);
        SetIconVisible(false);
        SetPromptVisible(false);
    }

    public TemplateContainer Template { get; }
    public IInteractable Interactable { get; }

    private Vector2 _Size =>
        new(Template.resolvedStyle.width, Template.resolvedStyle.height);
    private Vector2 _Center => new(_Size.x / 2, _Size.y / 2);

    private const string PROMPT_TEMPLATE_ID = "t_input-prompt_interact";
    private const string PROMPT_TEXT_ID = "v_label_prompt";
    private const string WAYPOINT_ICON_ID = "v_image_waypoint-icon";

    private void SetWaypointDetails()
    {
        Template.SetElementText(PROMPT_TEXT_ID, Interactable.InteractPrompt);
        Template.SetElementImage(WAYPOINT_ICON_ID, Interactable.WaypointImage);
    }

    public void SetWaypointVisible(bool isActive) =>
        Template.SetVisible(isActive);

    public void SetPromptVisible(bool isActive) =>
        Template.SetElementVisible(PROMPT_TEMPLATE_ID, isActive);

    public void SetIconVisible(bool isActive) =>
        Template.SetElementVisible(WAYPOINT_ICON_ID, isActive);

    public void UpdatePosition()
    {
        var worldPos = Interactable.Position;
        var offset = Interactable.WaypointOffset;
        var targetPos = worldPos + offset;
        // var waypointPos = Template.SetWorldToScreenSpacePosition(targetPos);

        var res = ScreenPositionUIExtensions.UiResolution;

        var minPos = _Center;
        Vector2 maxPos = new(res.x - minPos.x, res.y - minPos.y);

        var camera = ScreenPositionUIExtensions.MainCamera;
        var cameraPos = camera.WorldToViewportPoint(targetPos);
        Vector2 waypointPos = new(res.x * cameraPos.x, res.y * cameraPos.y);

        SetIconVisible(false);
        var behindCamera = Vector3.Dot(
            worldPos - camera.transform.position, camera.transform.forward
        ) < 0;
        if (behindCamera)
        {
            waypointPos.x = waypointPos.x > res.x / 2 ? minPos.x : maxPos.x;
        }

        waypointPos.x = Mathf.Clamp(waypointPos.x, minPos.x, maxPos.x);
        waypointPos.y = Mathf.Clamp(waypointPos.y, minPos.y, maxPos.y);

        if (waypointPos.x == minPos.x || waypointPos.x == maxPos.x)
        {
            var safeZoneY = res.y / 3;
            waypointPos.y = Mathf.Clamp(
                waypointPos.y, safeZoneY + minPos.y,
                res.y - safeZoneY - minPos.y
            );
            SetIconVisible(true);
        }

        // Screen height is bottom>up, but UI is top>down
        waypointPos.y = res.y - waypointPos.y;
        Template.transform.position = waypointPos;
    }
}
