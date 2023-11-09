using UnityEngine;

public abstract class SerializableScriptableObject : ScriptableObject
{
    protected void EditorWarning(string message)
    {
#if UNITY_EDITOR
        // Util.EditorWarning(message);
#endif
    }
}
