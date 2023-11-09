using UnityEngine;

public class ClumpSpawner : SpawnPoint<ClumpController>
{
    [Space, SerializeField]
    private Transform _cameraSystemPrefab;

    public override ClumpController Spawn()
    {
        Instantiate(_cameraSystemPrefab, _T.position, _T.rotation);
        return base.Spawn();
    }
}
