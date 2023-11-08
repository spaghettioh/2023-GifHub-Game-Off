using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadProcedures : MonoBehaviour
{
    /// <summary>
    ///     Primarily used by UnityEvents!
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName) =>
        SceneManager.LoadScene(sceneName);
}
