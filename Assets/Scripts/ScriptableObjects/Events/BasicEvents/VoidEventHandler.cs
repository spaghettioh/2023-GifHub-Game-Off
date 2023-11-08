using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventHandler : MonoBehaviour
{
    [SerializeField]
    private VoidEventSO _event;

    [SerializeField]
    private float _optionalDelay;

    [SerializeField]
    private UnityEvent _onEventRaised;

    private void OnEnable() => _event.OnEventRaised += OnEventRaised;

    private void OnDisable() => _event.OnEventRaised -= OnEventRaised;

    public void OnEventRaised()
    {
        if (_optionalDelay == 0f)
        {
            _onEventRaised.Invoke();
        }
        else
        {
            StartCoroutine(OnEventRaisedRoutine());
        }
    }

    private IEnumerator OnEventRaisedRoutine()
    {
        yield return new WaitForSeconds(_optionalDelay);
        _onEventRaised.Invoke();
    }
}
