using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UITweenHandler : MonoBehaviour
{
    private Transform _t;
    public Transform Transform => _t;
    public Vector3 Scale => _t.localScale;
    public Vector3 WorldPosition => _t.position;

    private void Awake() => TryGetComponent(out _t);

    public void SetPosition(Vector3 position) => _t.position = position;

    public void SetScale(Vector3 scale) => _t.localScale = scale;

    public UITweenHandler DoPath(List<Vector3> path, float duration)
    {
        _t.DOPath(path.ToArray(), duration, PathType.CatmullRom)
            .OnUpdate(() => this.OnUpdate())
            .OnComplete(() => this.OnComplete())
            .SetEase(Ease.OutCubic);
        return this;
    }

    public UITweenHandler DoScale(Vector3 endScale, float duration)
    {
        _t.DOScale(endScale, duration / 4f)
            .OnUpdate(() => this.OnUpdate())
            .OnComplete(() => this.OnComplete());
        return this;
    }

    public UITweenHandler DOBlendableMoveBy(Vector3 endPosition, float duration)
    {
        _t.DOBlendableMoveBy(endPosition, duration)
            .OnUpdate(() => this.OnUpdate())
            .OnComplete(() => this.OnComplete());
        return this;
    }
}
