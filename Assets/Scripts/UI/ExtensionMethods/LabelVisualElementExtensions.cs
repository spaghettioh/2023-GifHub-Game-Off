using UnityEngine;
using UnityEngine.UIElements;

public static class LabelVisualElementExtensions
{
    public static Label GetLabel(this VisualElement element, string id) =>
        element.Q<Label>(id);

    public static Label SetText(this Label label, string value)
    {
        label.text = value;
        return label;
    }

    public static Label SetText(this Label label, int value) =>
        label.SetText(value.ToString());

    public static Label SetText(this Label label, float value) =>
        label.SetText(value.ToString());

    public static Label SetElementText(
        this VisualElement parent, string id, string value
    )
    {
        var label = parent.GetLabel(id);
        return label.SetText(value);
    }

    public static Label SetElementText(
        this VisualElement element, string id, int value
    ) =>
        element.SetElementText(id, value.ToString());

    public static Label SetElementText(
        this VisualElement element, string id, float value
    ) =>
        element.SetElementText(id, value.ToString());

    public static Label SetElementText<T>(
        this VisualElement element, string id, T value
    ) =>
        element.SetElementText(id, value.ToString());

    public static void SetTextColor(this Label label, Color color)
    {
        label.style.color = new(color);
        // foreach (var mapping in colorMap.Map)
        // {
        //     text = text.Replace($"<{mapping.Tag}>", $"<color=#{hex}>");
        //     text = text.Replace($"</{mapping.Tag}>", "</color>");
        // }
        // return text;
    }
    // public static Label SetLabelColorStyle(
    //     this Label label,
    //     Color color
    // ) {
    //         var hex = ColorUtility.ToHtmlStringRGB(color);
    //     label.style.color = hex;
    //     foreach (var mapping in colorMap.Map)
    //     {
    //         text = text.Replace($"<{mapping.Tag}>", $"<color=#{hex}>");
    //         text = text.Replace($"</{mapping.Tag}>", "</color>");
    //     }
    //     return text;

    // }
}
