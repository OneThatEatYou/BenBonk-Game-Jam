using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    public SceneToBeLoaded[] scenes;

    public string[] unloadedSceneNames;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            SceneLoader.instance.LoadScene(scenes[i].sceneName, scenes[i].mode);
        }

        for (int i = 0; i < unloadedSceneNames.Length; i++)
        {
            SceneLoader.instance.UnloadScene(unloadedSceneNames[i]);
        }

        Destroy(gameObject);
    }
}

[System.Serializable]
public class SceneToBeLoaded
{
    public string sceneName;
    public LoadSceneMode mode;
}
