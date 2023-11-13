using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PropHandler : MonoBehaviour
{
    [SerializeField]
    private PropDataSO _propData;

    public bool IsAttaching { get; private set; }
    public bool IsCollectable { get; private set; }

    [SerializeField]
    private ClumpRuntimeDataSO _clumpData;

    [SerializeField]
    private float _attachDuration;

    [Header("Config")]
    [SerializeField]
    private TrailRenderer _uncollectTrail;

    [SerializeField]
    private Transform _originalParent;

    [Space]
    [SerializeField]
    private float _flickerDuration = 2f;

    private bool _isCollected;
    private Transform _t;
    private GameObject _attachPoint;

    private void Awake()
    {
        _originalParent = transform.parent;
        TryGetComponent(out _t);
        SetTrailActive(false);
    }

    public SoundEffectAudioDataSO SetCollected(AudioEventSO audioEvent)
    {
        // Disable colliders, turn off collectability, inform manager
        ToggleCollectable(false);
        // _colliders.ForEach(c => c.gameObject.SetActive(false));

        CreateAttachPoint();

        // Waits for one second to see if a crash will uncollect it
        _t.DOLocalMove(_t.localPosition, 1f).OnComplete(MoveTowardAttachPoint);

        return _propData.SoundEffect;
    }

    private void CreateAttachPoint()
    {
        // Create an object at the closest point to the collider
        _attachPoint = new("PropAttachPoint");
        var t = _attachPoint.transform;
        t.position = _clumpData.Collider.ClosestPoint(_t.position);
        t.SetParent(_clumpData.Transform);
        _t.SetParent(t);
        IsAttaching = true;
    }

    private void MoveTowardAttachPoint()
    {
        // Then move towards it
        _t.DOLocalMove(Vector3.zero, _attachDuration).OnComplete(
            () => {
                _t.SetParent(_clumpData.Transform);
                Destroy(_attachPoint);
                IsAttaching = false;
            }
        );
    }

    public void ToggleCollectable(bool onOff)
    {
        IsCollectable = onOff;
    }
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //

    public void DetachFromClump()
    {
        // Stop everything, like moving, or waiting
        StopAllCoroutines();
        _t.DOKill(true);

        // Return to the starting parent
        _t.SetParent(_originalParent);
        _t.rotation = Quaternion.identity;

        // Moves to a seemingly random position
        SetTrailActive(true);
        var localPos = transform.localPosition;
        var endPos = Vector3.zero;
        var random = Random.Range(-1, 2);
        endPos.x = localPos.x + 1 * random + Random.Range(-1f, 1f);
        endPos.z = localPos.z + 1 * random + Random.Range(-1f, 1f);
        //_transform.DOPunchScale(_transform.localScale * .5f, 1f, 1, 1f);
        _t.DOLocalJump(endPos, 3f, 2, 1f).OnComplete(
            () => {
                StartCoroutine(FlickerRoutine());
            }
        );
    }

    private IEnumerator FlickerRoutine()
    {
        // Flickers the sprite for some time
        var flickerTime = 0f;
        var alphaChange = Color.white;
        while (flickerTime < _flickerDuration)
        {
            alphaChange.a = alphaChange.a == 1 ? 0 : 1;
            var waitTime = Time.deltaTime + .1f;
            yield return new WaitForSeconds(waitTime);
            flickerTime += waitTime;
        }
        SetTrailActive(false);

        // Resets the prop
        ToggleCollectable(true);
    }

    private void SetTrailActive(bool active) =>
        _uncollectTrail.gameObject.SetActive(active);

    private void OnDrawGizmos()
    {
        var pos = transform.position;

        Color color;
        color = Color.green;
        color.a = .5f;
        Gizmos.color = color;
        Gizmos.DrawLine(pos + Vector3.down / 2, pos + Vector3.up / 2);

        color = Color.red;
        color.a = .5f;
        Gizmos.color = color;
        Gizmos.DrawLine(pos + Vector3.left / 2, pos + Vector3.right / 2);

        color = Color.blue;
        color.a = .5f;
        Gizmos.color = color;
        Gizmos.DrawLine(pos + Vector3.forward / 2, pos + Vector3.back / 2);
    }
}
