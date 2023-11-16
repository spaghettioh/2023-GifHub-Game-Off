using UnityEngine;
using UnityEngine.Serialization;

public class TransformAnchorSetProcedure : MonoBehaviour
{
    [SerializeField]
    private TransformAnchorSO _anchor;

    [FormerlySerializedAs("_target")]
    [SerializeField]
    private Transform _targetParent;

    private void Awake()
    {
        if (_targetParent != null)
        {
            _anchor.SetTransform(_targetParent);
            return;
        }
        _anchor.SetTransform(transform);
    }
}
