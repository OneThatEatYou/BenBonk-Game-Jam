using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class EndLight : MonoBehaviour
{
    public Image image;
    public float fadeoutTime;
    public AudioMixer masterMixer;

    public TextMeshProUGUI text;
    public string[] endTexts;
    public float timeBetweenSlides;

    public GameObject prompt;
    bool ended = false;

    public Image transitionImg;

    float t;
    bool isTriggered;

    private void Update()
    {
        if (ended)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(MainMenuTransition());
                ended = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isTriggered)
            {
                StartCoroutine(FadeOut());
                isTriggered = true;

                collision.GetComponent<PlayerController>().canMove = false;
            }
        } 
    }


    IEnumerator FadeOut()
    {
        Color col = image.color;
        float v;
        t = 0f;

        while (image.color.a != 1)
        {
            col.a = Mathf.Lerp(0, 1, t / fadeoutTime);
            image.color = col;

            v = Mathf.SmoothStep(0, -80, t / fadeoutTime);
            masterMixer.SetFloat("MasterVolume", v);

            t += Time.deltaTime;

            yield return null;
        }

        for (int i = 0; i < endTexts.Length; i++)
        {
            text.text = "";
            text.text = endTexts[i];

            yield return new WaitForSeconds(timeBetweenSlides);
        }

        prompt.SetActive(true);
        ended = true;
    }

    IEnumerator MainMenuTransition()
    {
        Color col = transitionImg.color;
        t = 0f;

        while (transitionImg.color.a != 1)
        {
            col.a = Mathf.Lerp(0, 1, t);
            transitionImg.color = col;

            t += Time.deltaTime;

            yield return null;
        }

        SceneLoader.instance.LoadScene("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
