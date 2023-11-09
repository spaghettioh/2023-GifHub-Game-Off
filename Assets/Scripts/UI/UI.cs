using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public abstract class UI : MonoBehaviour
{
    // public UIDocument Document { get; private set; }
    public VisualElement _Root { get; private set; }

    /// <summary>
    ///     Just grabs the document component and sets the root.
    /// </summary>
    protected virtual void Awake()
    {
        TryGetComponent(out UIDocument document);
        _Root = document.rootVisualElement;
        HideUI();
    }

    private void Start()
    {
        HideUI();
        OnUIStart();
    }

    protected virtual void OnUIStart() { }

    protected void ShowUI() => _Root.SetVisible(true);

    protected void HideUI() => _Root.SetVisible(false);

    // FR I want to get an element without having to call document root every
    // time
}
