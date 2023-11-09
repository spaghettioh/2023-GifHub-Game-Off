using UnityEngine;

[CreateAssetMenu(
    fileName = "Pool_FXObject",
    menuName = "Gameplay/FX/FX object pool"
)]
public class VFXHandlerPoolSO : ComponentPool<VFXHandler>
{
    protected override void OnRequest(VFXHandler requested)
    {
        base.OnRequest(requested);
        requested.gameObject.SetActive(true);
    }
}
