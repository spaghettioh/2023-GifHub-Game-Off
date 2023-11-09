using System.Collections;
using Nerdscape.Events;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [Header("Listening for...")]
    [SerializeField]
    private EventBase<int> _event;

    [SerializeField]
    private float _optionalDelay;

    [SerializeField]
    private UnityEvent _onEventRaised;

    private void OnEnable() => _event.OnEventRaised += HandleEventRaised;

    private void OnDisable() => _event.OnEventRaised -= HandleEventRaised;

    public void HandleEventRaised(int _) =>
        StartCoroutine(OnEventRaisedRoutine());

    private IEnumerator OnEventRaisedRoutine()
    {
        if (_optionalDelay != 0f)
        {
            yield return new WaitForSeconds(_optionalDelay);
        }
        _onEventRaised.Invoke();
    }
}
