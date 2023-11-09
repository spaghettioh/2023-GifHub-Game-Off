using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class HUDCounter
{
    [field: SerializeField]
    public string TemplateId { get; protected set; }

    [SerializeField]
    protected string _LabelId;

    [SerializeField]
    protected float _PulseDuration = 1f;

    protected Label _Label;
    protected TemplateContainer _Template;
    protected UITweenHandler TweenHandler;

    public virtual void Initialize(
        VisualElement parent, UITweenHandler tweenHandler
    )
    {
        TweenHandler = tweenHandler;
        _Template = parent.GetTemplate(TemplateId);
        _Label = _Template.GetLabel(_LabelId);
    }

    public virtual void SetValue(float amount)
    {
        if (_Label == null)
        {
            return;
        }
        _Label.SetText(amount);
        DoPulseEffect();
    }

    protected virtual void DoPulseEffect() { }

    protected virtual void SetElementPosition(VisualElement element) =>
        element.transform.position = TweenHandler.Transform.position;

    protected virtual void SetElementScale(VisualElement element) =>
        element.transform.scale = TweenHandler.Transform.localScale;

    protected virtual void SetElementAlpha(VisualElement element)
    {
        // TODO CLEANUP Use normalize static method
        var normalized =
            (TweenHandler.Transform.localScale.x - 1.5f) / (1f - 1.5f);
        element.style.opacity = normalized;
    }
}
