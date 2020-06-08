using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image transitionImg;
    float t;
    public float fadeinTime = 1f;
    public AudioMixer mainMixer;
    float v;

    public GameObject wallTorchPrefab;

    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    private void Start()
    {
        StartCoroutine(PlayTransition());
    }

    IEnumerator PlayTransition()
    {
        Color col = transitionImg.color;
        t = 0f;

        while (transitionImg.color.a != 0)
        {
            col.a = Mathf.Lerp(1, 0, t / fadeinTime);
            transitionImg.color = col;

            v = Mathf.SmoothStep(-10, 0, t / fadeinTime);
            mainMixer.SetFloat("MasterVolume", v);

            t += Time.deltaTime;

            yield return null;
        }
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

                //torches[i].isThrown = false;

                //Debug.Log(torches[i].torchStartPos);
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
