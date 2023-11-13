public interface IUIFadable
{
    UIFadeEventSO FadeEvent { get; }
    void FadeIn(IUIFadable ui);
    void FadeOut(IUIFadable ui);
}
