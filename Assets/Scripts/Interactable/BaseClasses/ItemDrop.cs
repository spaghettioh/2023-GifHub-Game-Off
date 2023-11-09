using System;
using UnityEngine;

public abstract class ItemDrop : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if (!_spriteRenderer.IsNull())
        {
            return;
        }

        if (!TryGetComponent(out _spriteRenderer))
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (_spriteRenderer.IsNull())
        {
            throw new NotImplementedException();
        }

        OnAwake();
    }

    protected virtual void OnAwake() { }

    protected void InitializeDrop(string itemName, Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        name = itemName.GetNameTag();
    }
}
