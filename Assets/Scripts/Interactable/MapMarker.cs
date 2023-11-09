using UnityEngine;

public class MapMarker : MonoBehaviour
{
    [field: SerializeField]
    public Texture2D Icon { get; private set; }

    [SerializeField]
    private Transform _transform;
    public Vector3 Position => _transform.position;
    public Vector3 Angles => _transform.eulerAngles;

    private void Awake()
    {
        if (_transform.IsNull())
        {
            TryGetComponent(out _transform);
        }
    }
}
