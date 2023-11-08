using UnityEngine;
using UnityEngine.UIElements;

public static class ImageVisualElementExtensions
{
    public static void SetElementImage(
        this VisualElement element, string id, Sprite sprite
    ) =>
        element.GetElementStyle(id).backgroundImage = new(sprite);

    public static void SetElementTint(
        this VisualElement element, string id, Color color
    ) =>
        element.GetElementStyle(id).unityBackgroundImageTintColor = new(color);

    public static void SetImage(this VisualElement element, Sprite sprite) =>
        element.style.backgroundImage = new(sprite);
    public static void
        SetImage(this VisualElement element, Texture2D texture) =>
        element.style.backgroundImage = new(texture);
}
