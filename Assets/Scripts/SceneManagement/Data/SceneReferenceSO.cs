using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Scene_NAME", menuName = "Data/Scene reference")]
public class SceneReferenceSO : ScriptableObject
{
    public enum Type
    {
        Location,
        Menu,
    }

    [field: SerializeField]
    public AssetReference Scene { get; private set; }

    [field: SerializeField]
    public MusicAudioDataSO Music { get; private set; }

    [field: SerializeField]
    public Type SceneType { get; private set; }
}
