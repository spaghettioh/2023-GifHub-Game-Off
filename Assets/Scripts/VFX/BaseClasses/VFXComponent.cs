using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VFXComponent : MonoBehaviour
{
    public event Action<VFXComponent> OnVFXFinished;
    [field: SerializeField]
    public VFXType Type { get; protected set; }

    [field: SerializeField]
    public SoundEffectAudioDataSO SoundEffect { get; protected set; }

    protected Transform _T;

    private VisualEffect _visualEffect;
    public VisualEffect VisualEffect => _visualEffect;

    private void Awake()
    {
        TryGetComponent(out _T);
        TryGetComponent(out _visualEffect);
    }
    public IEnumerator VisualEffectRoutine(
        VFXType vfxType, Transform targetTransform
    )
    {
        VisualEffect.Play();
        yield return new WaitForSeconds(.1f);

        while (VisualEffect.HasAnySystemAwake())
        {
            if (vfxType is VFXType.None)
            {
                _T.SetPositionAndRotation(
                    targetTransform.position, targetTransform.rotation
                );
            }
            yield return null;
        }

        VisualEffect.Stop();
        OnVFXFinished?.Invoke(this);
    }
}
