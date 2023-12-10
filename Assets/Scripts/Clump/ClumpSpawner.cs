using UnityEngine;

public class ClumpSpawner : SpawnPoint<ClumpMovementSystem>
{
    [Space]
    [SerializeField]
    private Transform _cameraSystemPrefab;

    public override ClumpMovementSystem Spawn()
    {
        Instantiate(_cameraSystemPrefab, _T.position, _T.rotation);
        return base.Spawn();
    }
}
