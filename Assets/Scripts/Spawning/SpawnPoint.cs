using System;
using UnityEngine;

[Serializable]
public abstract class SpawnPoint<T> : MonoBehaviour
    where T : Component
{
    [SerializeField]
    private T _prefab;

    [SerializeField]
    protected TransformAnchorSO _Parent;

    protected Transform _T;

    private void Awake() => TryGetComponent(out _T);

    public virtual T Spawn()
    {
        var spawned = Instantiate(
            _prefab, _T.position, _T.rotation, _Parent?.Transform
        );
        spawned.name = spawned.name.GetNameTag();
        Destroy(gameObject, 1f);
        return spawned;
    }
}
