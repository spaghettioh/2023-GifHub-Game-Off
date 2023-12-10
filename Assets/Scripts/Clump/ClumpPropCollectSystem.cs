using UnityEngine;

public class ClumpPropCollectSystem : MonoBehaviour
{
    [SerializeField]
    private SphereCollider _collider;
    private float _Size => _collider.radius * 2;

    private void OnTriggerEnter(Collider other) => TryCollectProp(other);

    private void OnCollisionEnter(Collision other) =>
        TryCollectProp(other.collider);

    private bool TryCollectProp(Collider other)
    {
        if (!other.TryGetComponent(out PropHandler prop))
        {
            return false;
        }
        return ComparePropSize(prop);
    }

    private bool ComparePropSize(PropHandler prop)
    {
        var requiredDiameter = prop.PropData.SizeToCollect;
        // Check the diameter against the prop required size; 
        if (_Size < requiredDiameter)
        {
            Debug.Log($"{prop.name} is too big");
            PropEvent.RaiseCrash(prop, name);
            return false;
        }
        Debug.Log($"{prop.name} is small enough");
        PropEvent.RaiseCollected(prop, name);
        AddProp(requiredDiameter);
        return true;
    }

    private void AddProp(float requiredSize)
    {
        var startSize = _Size;
        Debug.Log($"Start size: {startSize}");
        //
        // if (startSize == requiredSize)
        // {
        //     startSize *= 1.02154f;
        // }

        // var endSize = startSize + (startSize - requiredSize);
        var endSize = startSize + requiredSize;
        _collider.radius = endSize / 2f;
    }
}
