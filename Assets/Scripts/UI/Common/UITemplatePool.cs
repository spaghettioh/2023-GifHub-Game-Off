using UnityEngine;
using UnityEngine.UIElements;

public abstract class
    UITemplatePool : PoolBase<TemplateContainer, VisualElement>
{
    [SerializeField]
    private VisualTreeAsset _templateAsset;

    private VisualElement _root;

    public override void PreWarm(VisualElement root)
    {
        _root = root;
        InitializePool();
    }

    protected override TemplateContainer Create()
    {
        var template = _templateAsset.CloneTree();
        _root.Add(template);
        template.SetDisplay(false);
        return template;
    }

    protected override void OnRequest(TemplateContainer template)
    {
        template.SetDisplay(true);
    }

    protected override void OnRelease(TemplateContainer template)
    {
        template.SetDisplay(false);
    }
}
