using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Storage of elements opened with keys from owners
/// </summary>
/// <typeparam name="TOwner">
///     The key owner
/// </typeparam>
/// <typeparam name="TEntity">
///     The entity an owner owns
/// </typeparam>
public class Vault<TOwner, TEntity>
{
    private int _nextUniqueKey;
    public List<VaultKey> Keys { get; }
    public List<TEntity> Entities { get; }

    public Vault()
    {
        Keys = new();
        Entities = new();
    }

    private VaultKey GetNewKey(TOwner owner) =>
        new(_nextUniqueKey++, owner as Type);

    public bool ContainsOwner(TOwner owner)
    {
        List<VaultKey> results = Keys.FindAll(key => key.Owner.Equals(owner));
        return results.Count > 0;
    }

    public VaultKey Add(TOwner owner, TEntity entity)
    {
        var key = GetNewKey(owner);

        Keys.Add(key);
        Entities.Add(entity);

        return key;
    }

    public bool TryGetEntity(VaultKey key, out TEntity entity)
    {
        var index = Keys.FindIndex(k => k.Equals(key));
        entity = default;

        if (index < 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning(
                $"Couldn't return value for key ({key.Owner}, {key.Id})"
            );
#endif
            return false;
        }
        entity = Entities[index];
        return true;
    }

    public TEntity Remove(VaultKey key)
    {
        if (TryGetEntity(key, out var entity))
        {
            var keyIndex = Keys.FindIndex(k => k == key);
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

        Keys.RemoveAt(index);
        Entities.RemoveAt(index);

        return true;
    }
}
