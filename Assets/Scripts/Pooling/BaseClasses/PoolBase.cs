using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TPooledItem">
///     Object to pool
/// </typeparam>
/// <typeparam name="TPoolParent">
///     Parent object
/// </typeparam>
public abstract class PoolBase<TPooledItem, TPoolParent> : ScriptableObject
    where TPooledItem : class
{
    [SerializeField]
    protected TPooledItem Object;

    [SerializeField]
    private int _poolSize;

    protected ObjectPool<TPooledItem> Pool;

    /// <summary>
    ///     * Be sure to call Initialize()!
    /// </summary>
    /// <param name="parent"></param>
    public abstract void PreWarm(TPoolParent parent);

    /// <summary>
    ///     Spins up an object pool of objects
    /// </summary>
    /// <param name="count">
    ///     The number of objects to create
    /// </param>
    protected virtual void InitializePool()
    {
        Pool = new(Create, OnRequest, OnRelease, null, true, _poolSize);
        CreateItems();
    }

    private void CreateItems()
    {
        List<TPooledItem> items = new();
        for (var i = 0; i < _poolSize; i++)
        {
            items.Add(Pool.Get());
        }

        foreach (var item in items)
        {
            Pool.Release(item);
        }
    }

    protected abstract TPooledItem Create();

    /// <summary>
    ///     Called when an item is requested from the pool via Get()
    /// </summary>
    /// <returns></returns>
    protected abstract void OnRequest(TPooledItem item);

    /// <summary>
    ///     Called when an item is released back into the pool via Release()
    /// </summary>
    /// <returns></returns>
    protected abstract void OnRelease(TPooledItem item);

    /// <summary>
    ///     Called when the pool or item is destroyed
    /// </summary>
    /// <returns></returns>
    // protected virtual void OnDestroy(T1 item) { }
    public virtual TPooledItem Request() => Pool.Get();

    public virtual void Return(TPooledItem item) => Pool.Release(item);

    public virtual void Clear()
    {
        Pool?.Clear();
    }

    private void OnDisable()
    {
        Clear();
    }
}
