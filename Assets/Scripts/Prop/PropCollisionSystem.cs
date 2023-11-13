using System;
using System.Collections.Generic;
using UnityEngine;

public class PropCollisionSystem : MonoBehaviour
{
    public static event Action<Collider> OnCollision;

    [SerializeField]
    private List<SphereCollider> _colliders;

    public void SetIsTrigger(bool isTrigger) =>
        _colliders.ForEach(c => c.isTrigger = isTrigger);

    private void OnCollisionEnter(Collision collision) =>
        OnCollision.CheckSubscriptions(collision.collider, name);

    private void OnTriggerEnter(Collider other) =>
        OnCollision.CheckSubscriptions(other, name);
}
