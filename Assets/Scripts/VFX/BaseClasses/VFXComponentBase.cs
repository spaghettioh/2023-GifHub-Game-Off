using UnityEngine;
using UnityEngine.VFX;

public abstract class VFXComponentBase : MonoBehaviour
{
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
}
