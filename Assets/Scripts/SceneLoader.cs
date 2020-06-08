using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Singleton

    public static SceneLoader instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public void LoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadSceneAsync(sceneName, mode);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
