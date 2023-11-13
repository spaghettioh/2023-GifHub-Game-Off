using UnityEngine.UIElements;

public static class SpacerVisualElementExtensions
{
    public static void AddSpacerSmall(this VisualElement parent) =>
        parent.AddSpacer("t_spacer_small");

    public static void AddSpacerMedium(this VisualElement parent) =>
        parent.AddSpacer("t_spacer_medium");

    public static void AddSpacerLarge(this VisualElement parent) =>
        parent.AddSpacer("t_spacer_large");

    public static void AddSpacerXLarge(this VisualElement parent) =>
        parent.AddSpacer("t_spacer_x-large");

    public static void AddSpacer(this VisualElement parent, string spacerName)
    {
        var spacer = new VisualElement();
        spacer.name = spacerName;
        parent.Add(spacer);
    }
}
