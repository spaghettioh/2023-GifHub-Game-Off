using UnityEngine;

public class ClumpSpawner : SpawnPoint<ClumpMovementController>
{
    [Space]
    [SerializeField]
    private Transform _cameraSystemPrefab;

    public override ClumpMovementController Spawn()
    {
        Instantiate(_cameraSystemPrefab, _T.position, _T.rotation);
        return base.Spawn();
    }
}
