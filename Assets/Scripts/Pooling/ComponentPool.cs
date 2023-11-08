using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ComponentPool<T> : PoolBase<T, Transform>
    where T : Component
{
    private Transform _parent;

    public override void PreWarm(Transform parent)
    {
        _parent = parent;
        InitializePool();
    }

    protected override void OnRequest(T requested)
    {
        requested.transform.SetParent(_parent);
    }

    protected override void OnRelease(T returning)
    {
        returning.gameObject.SetActive(false);
        ResetParent(returning);
    }

    // protected override void OnDestroy(T item)
    // {
    //     throw new System.NotImplementedException();
    // }

    private void ResetParent(T returning)
    {
        returning.transform.SetParent(_parent);
    }

    /// <summary>
    /// Instantiates an object and adds it to the pool
    /// </summary>
    /// <returns>An object</returns>
    protected override T Create()
    {
        T t = Instantiate(Object, _parent);
        t.gameObject.SetActive(false);

        return t;
    }
}
