using System.Collections.Generic;
using UnityEngine;

public class LootCollectTest : MonoBehaviour
{
    public Transform _target;

    public GameObject loot;
    public ParticleSystem particle;
    public float _moveSpeed;
    public float distancer = 2f;
    public float _drag;
    public bool dragIncrease;
    public float _maxRange;
    public ForceMode mode = ForceMode.Force;

    private readonly List<Rigidbody> spawned = new();
    public float timeScale;

    private Vector3 lootSpawnPosition;

    // Start is called before the first frame update
    private void Start()
    {
        Spawn();
        var shape = particle.shape;
        lootSpawnPosition = transform.position;
        shape.position = lootSpawnPosition;
    }

    private void Update()
    {
        transform.position = _target.position;
        var shape = particle.shape;
        shape.position = lootSpawnPosition - _target.position;
        Time.timeScale = timeScale;
        for (var i = spawned.Count - 1; i > 0; i--)
        {
            var target = _target.position + Vector3.forward * 2;
            var position = spawned[i].transform.position;
            var direction = target - position;
            var distance = direction.magnitude;
            var force = _moveSpeed / distance;
            if (dragIncrease)
            {
                spawned[i].drag =
                    Mathf.InverseLerp(10f, 2f, distance - 2) * _drag;
            }
            else
            {
                spawned[i].drag = _drag;
            }

            var distanceScale = Mathf.InverseLerp(_maxRange, 0f, distance);
            var attractionStrength = Mathf.Lerp(0f, _moveSpeed, distanceScale);

            spawned[i].AddForce(
                direction.normalized * attractionStrength, mode
            );

            if (distance < 2)
            {
                spawned[i].gameObject.SetActive(false);
                spawned.Remove(spawned[i]);
            }
        }
    }

    private void Spawn()
    {
        spawned.Add(
            Instantiate(loot, transform.position, Quaternion.identity)
                .GetComponent<Rigidbody>()
        );
        Invoke(nameof(Spawn), 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 2f);
    }
}
