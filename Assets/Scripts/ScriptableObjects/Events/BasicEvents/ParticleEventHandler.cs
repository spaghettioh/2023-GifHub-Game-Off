using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEventHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent _onStopped;
    private void OnParticleSystemStopped()
    {
        _onStopped?.Invoke();
    }
}
