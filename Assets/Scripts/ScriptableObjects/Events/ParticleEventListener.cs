using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEventListener : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onStopped;
    private void OnParticleSystemStopped()
    {
        _onStopped?.Invoke();
    }
}
