using UnityEngine;

[CreateAssetMenu(
    fileName = "Pool_UITweenHandler", menuName = "UI/Tween handler pool"
)]
public class UITweenHandlerPoolSO : ComponentPool<UITweenHandler>
{
    protected override void OnRequest(UITweenHandler requested)
    {
        base.OnRequest(requested);
        requested.gameObject.SetActive(true);
    }
}
