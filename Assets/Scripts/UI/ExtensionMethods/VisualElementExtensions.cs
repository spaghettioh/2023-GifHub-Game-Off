using UnityEngine.UIElements;

public static class VisualElementExtensions
{
    private const string DISPLAY_NONE = "display-none";
    private const string VISIBILITY_HIDDEN = "visibility-hidden";

    public static VisualElement GetElement(
        this VisualElement element, string id
    ) =>
        element.Q<VisualElement>(id);

    public static TemplateContainer GetTemplate(
        this VisualElement element, string id
    ) =>
        element.Q<TemplateContainer>(id);

    public static IStyle GetElementStyle(
        this VisualElement element, string id
    ) =>
        element.GetElement(id).style;

    public static void SetElementDisplay(
        this VisualElement element, string id, bool isActive
    )
    {
        if (isActive)
        {
            element.GetElement(id).RemoveFromClassList(DISPLAY_NONE);
            return;
        }
        element.GetElement(id).AddToClassList(DISPLAY_NONE);
    }

    /// <summary>
    ///     /// Sets the 'display' style property on the element.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="isActive">
    ///     Whether or not the element should display.
    /// </param>
    public static void SetDisplay(this VisualElement element, bool isActive)
    {
        if (isActive)
        {
            element.RemoveFromClassList(DISPLAY_NONE);
            return;
        }
        element.AddToClassList(DISPLAY_NONE);
    }

    public static void SetElementVisible(
        this VisualElement element, string id, bool isVisible
    )
    {
        if (isVisible)
        {
            element.GetElement(id).RemoveFromClassList(VISIBILITY_HIDDEN);
            return;
        }
        element.GetElement(id).AddToClassList(VISIBILITY_HIDDEN);
    }

    /// <summary>
    ///     Sets the 'visibility' style property on the element.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="isVisible">
    ///     Whether or not the element should display.
    /// </param>
    public static void SetVisible(this VisualElement element, bool isVisible)
    {
        if (isVisible)
        {
            element.RemoveFromClassList(VISIBILITY_HIDDEN);
            return;
        }
        element.AddToClassList(VISIBILITY_HIDDEN);
    }

    public static void SetAbsolutePosition(
        this VisualElement element, Length top, Length bottom, Length left,
        Length right
    )
    {
        element.style.position = Position.Absolute;
        element.style.top = top;
        element.style.bottom = bottom;
        element.style.left = left;
        element.style.right = right;
    }

    public static void SetAbsolutePosition(
        this VisualElement element, VisualElement target
    )
    {
        var res = target.resolvedStyle;
        element.SetAbsolutePosition(res.top, res.bottom, res.left, res.right);
    }

    public static void SetTargetPosition(
        this VisualElement element, VisualElement target
    )
    {
        element.style.position = Position.Absolute;
        element.style.left = target.resolvedStyle.left;
        element.style.top = target.resolvedStyle.top;
        element.style.height = target.resolvedStyle.height;
        element.style.width = target.resolvedStyle.width;
    }
}
