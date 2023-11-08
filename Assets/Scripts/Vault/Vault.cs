using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Storage of elements opened with keys from owners
/// </summary>
/// <typeparam name="TOwner">The key owner</typeparam>
/// <typeparam name="TEntity">The entity an owner owns</typeparam>
public class Vault<TOwner, TEntity>
{
    private int _nextUniqueKey = 0;
    private List<VaultKey> _keys;
    public List<VaultKey> Keys => _keys;
    private List<TEntity> _entities;
    public List<TEntity> Entities => _entities;

    public Vault()
    {
        _keys = new();
        _entities = new();
    }

    private VaultKey GetNewKey(TOwner owner)
    {
        return new VaultKey(_nextUniqueKey++, owner as Type);
    }

    public bool ContainsOwner(TOwner owner)
    {
        var results = _keys.FindAll(key => key.Owner.Equals(owner));
        return results.Count > 0;
    }

    public VaultKey Add(TOwner owner, TEntity entity)
    {
        VaultKey key = GetNewKey(owner);

        _keys.Add(key);
        _entities.Add(entity);

        return key;
    }

    public bool TryGetEntity(VaultKey key, out TEntity entity)
    {
        int index = _keys.FindIndex(k => k.Equals(key));
        entity = default(TEntity);

        if (index < 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning(
                $"Couldn't return value for key ({key.Owner}, {key.Id})"
            );
#endif
            return false;
        }
        entity = _entities[index];
        return true;
    }

    public TEntity Remove(VaultKey key)
    {
        if (TryGetEntity(key, out var entity))
        {
            var keyIndex = _keys.FindIndex(k => k == key);
            RemoveAt(keyIndex);
        }
        return entity;
    }

    private bool RemoveAt(int index)
    {
        if (index < 0)
        {
            return false;
        }

        _keys.RemoveAt(index);
        _entities.RemoveAt(index);

        return true;
    }
}
