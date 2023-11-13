using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField]
    private VFXHandlerPoolSO _vfxHandlerPool;

    [Header("Listening for...")]
    [SerializeField]
    private VFXEventSO _vfxEvent;

    private void Start() => _vfxHandlerPool.PreWarm(transform);

    private void OnEnable()
    {
        _vfxEvent.OnEventRaised += HandleVFXEvent;
    }

    private void OnDisable()
    {
        _vfxEvent.OnEventRaised -= HandleVFXEvent;
    }

    private void HandleVFXEvent(VFXType type, Transform target)
    {
        var handler = GetHandlerFromPool();
        handler.transform.position = target.position;
        handler.DoVFX(type, target);
    }

    private VFXHandler GetHandlerFromPool()
    {
        var handler = _vfxHandlerPool.Request();
        handler.OnHandlerFinished += HandleFXFinished;
        return handler;
    }

    private void HandleFXFinished(VFXHandler handler)
    {
        handler.OnHandlerFinished -= HandleFXFinished;
        _vfxHandlerPool.Return(handler);
    }
}
