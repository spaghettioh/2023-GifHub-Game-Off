using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ClumpData", menuName = "Game Off/ClumpData")]
public class ClumpRuntimeDataSO : ScriptableObject
{
    public event Action<int> OnPropCountChanged;
    public event Action<float> OnStatsChanged;

    [field: SerializeField]
    public float MaxSpeed { get; private set; }
    [field: SerializeField]
    public float MinColliderRadius { get; private set; }
    [field: SerializeField]
    public float MaxColliderRadius { get; private set; }
    [field: SerializeField]
    public float MinMoveForce { get; private set; }
    [field: SerializeField]
    public float MaxMoveForce { get; private set; }

    [Header("DEBUG ==========")]
    [SerializeField]
    private string _header;
    [field: SerializeField]
    public SphereCollider Collider { get; private set; }
    [field: SerializeField]
    public Transform Transform { get; private set; }
    [field: SerializeField]
    public Rigidbody Body { get; private set; }
    public Vector3 Euler => Transform.eulerAngles;
    public Vector3 Position => Transform.position;
    [field: SerializeField]
    public int CollectedCount { get; private set; }
    [field: SerializeField]
    public Vector3 Velocity => Body.velocity;
    public float CurrentSpeed => Velocity.magnitude;
    [field: SerializeField]
    public float MoveForce { get; private set; }

    public void ConfigureData(Transform t, Rigidbody b, SphereCollider c)
    {
        Transform = t;
        Body = b;
        Collider = c;

        MoveForce = MinMoveForce;
        Collider.radius = MinColliderRadius;
        CollectedCount = 0;
    }

    public void IncreaseSize(float radius, float force)
    {
        MoveForce += force;
        Collider.radius += radius;
        CollectedCount++;
        RaiseStatsChange();
    }

    public void DecreaseSize(float radius, float force)
    {
        MoveForce -= force;
        Collider.radius -= radius;
        CollectedCount--;
        RaiseStatsChange();
    }

    private void RaiseStatsChange()
    {
        OnPropCountChanged.CheckSubscriptions(CollectedCount, name);
        OnStatsChanged.CheckSubscriptions(MoveForce, name);
    }
}
