using UnityEngine;

public class HUDUI : UI
{
    // [field: SerializeField]
    // public InventoryRuntimeDataSO Inventory { get; private set; }

    [field: SerializeField]
    public UITweenHandlerPoolSO TweenHandlerPool { get; private set; }

    [Header("Listening for...")]
    [SerializeField]
    private UIFadeEventSO _fadeEvent;
    public UIFadeEventSO FadeEvent => _fadeEvent;

    protected override void Awake()
    {
        base.Awake();
        TweenHandlerPool.PreWarm(transform);
    }

    protected override void OnUIStart() => ShowUI();

    private void OnEnable()
    {
        _fadeEvent.OnFadeIn += FadeIn;
        _fadeEvent.OnFadeOut += FadeOut;
    }

    private void OnDisable()
    {
        _fadeEvent.OnFadeIn -= FadeIn;
        _fadeEvent.OnFadeOut -= FadeOut;
    }

    protected UITweenHandler GetTweener(string name)
    {
        var tweener = TweenHandlerPool.Request();
        tweener.name = name;
        return tweener;
    }

    public void FadeIn(IUIFadable ui)
    {
        // if (ui as HUDRegion == this)
        // {
        Debug.Log($"{name} wants to fade in");
        // }
    }

    public void FadeOut(IUIFadable ui)
    {
        // if (ui as HUDRegion == this)
        // {
        Debug.Log($"{name} wants to fade out");
        // }
    }
}
