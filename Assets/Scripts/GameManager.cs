using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject wallTorchPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void ResetScene()
    {
        //resets thrown torches
        Torch[] torches = FindObjectsOfType<Torch>();

        for (int i = 0; i < torches.Length; i++)
        {
            if (torches[i].torchStartPos != null)
            {
                Instantiate(wallTorchPrefab, torches[i].torchStartPos, Quaternion.identity);
                Destroy(torches[i].gameObject);

                Debug.Log(torches[i].torchStartPos);
            }
        }

        HandTorch handTorch = FindObjectOfType<HandTorch>();

        if (handTorch)
        {
            Instantiate(wallTorchPrefab, handTorch.torchStartPos, Quaternion.identity);
            Destroy(handTorch.gameObject);
        }
        
    }
}
