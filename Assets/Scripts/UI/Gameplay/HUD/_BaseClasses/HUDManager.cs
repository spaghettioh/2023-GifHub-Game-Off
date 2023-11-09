using UnityEngine;

[RequireComponent(typeof(HUDUI))]
public abstract class HUDManager : MonoBehaviour
{
    protected HUDUI _HUD;

    private void Awake() => TryGetComponent(out _HUD);

    protected void SetUpCounter(HUDCounter counter)
    {
        var handler = GetTweenHandler(counter.TemplateId);
        counter.Initialize(_HUD._Root, handler);
    }

    private UITweenHandler GetTweenHandler(string name)
    {
        var handler = _HUD.TweenHandlerPool.Request();
        handler.name = name;
        return handler;
    }
}
