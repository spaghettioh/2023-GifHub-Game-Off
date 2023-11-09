using UnityEngine;
using UnityEngine.UIElements;

public class ReticleUI : UI
{
    [Header("Inner")]
    [SerializeField]
    private VisualTreeAsset _innerAsset;

    [SerializeField]
    private VisualTreeAsset _innerLockOnAsset;

    [SerializeField]
    private TransformAnchorSO _innerWorldTransform;
    private TemplateContainer _innerTemplate;
    private TemplateContainer _innerLockOnTemplate;

    [Header("Outer")]
    [SerializeField]
    private VisualTreeAsset _outerAsset;

    [SerializeField]
    private VisualTreeAsset _outerLockOnAsset;

    [SerializeField]
    private TransformAnchorSO _outerWorldTransform;
    private TemplateContainer _outerTemplate;
    private TemplateContainer _outerLockOnTemplate;

    protected override void Awake()
    {
        base.Awake();
        // TODO add all VEs and hide when not in use
        // TODO convert all reticles to VEs, send them their template from this document
        _innerTemplate = _innerAsset.CloneTree();
        _innerLockOnTemplate = _innerAsset.CloneTree();
        _outerTemplate = _outerAsset.CloneTree();
        _outerLockOnTemplate = _outerAsset.CloneTree();
        _Root.Add(_innerTemplate);
        _Root.Add(_outerTemplate);
        // TODO FR I want to lock-on to mobs with the lock-on laser
        // add event for lock-on targeting
        // DocumentRoot.Add(_innerLockOnTemplate);
    }

    protected override void OnUIStart() => ShowUI();

    private void FixedUpdate()
    {
        if (_innerWorldTransform.Transform.IsNull())
        {
            return;
        }
        _innerTemplate.SetWorldToScreenSpacePosition(
            _innerWorldTransform.Transform.position, false
        );
        _outerTemplate.SetWorldToScreenSpacePosition(
            _outerWorldTransform.Transform.position, false
        );
    }
}
