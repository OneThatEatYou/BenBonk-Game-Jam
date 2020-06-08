using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public Image transitionImg;
    public float fadeoutTime = 5f;
    float t;
    public SceneChangeTrigger trigger;

    public AudioSource source;
    public AudioClip buttonClip;
    public AudioClip[] sfxClips;

    [Header("Audio Settings")]
    public AudioMixer mainMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;
    float bgmVol;
    float sfxVol;

    private void Start()
    {
        mainMixer.SetFloat("MasterVolume", 0);

        mainMixer.GetFloat("BGMVolume", out bgmVol);
        mainMixer.GetFloat("SFXVolume", out sfxVol);
        bgmSlider.value = bgmVol;
        sfxSlider.value = sfxVol;

        StartCoroutine(Fadein());
    }

    public void PlaySFX()
    {
        source.clip = buttonClip;
        source.Play();
    }

    public void PlayRandomSFX()
    {
        source.clip = sfxClips[Random.Range(0, sfxClips.Length)];
        source.Play();
    }

    public void ChangeBGMVolume(float volume)
    {
        mainMixer.SetFloat("BGMVolume", volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVolume", volume);
    }

    public void Play()
    {
        StartCoroutine(PlayTransition());
    }

    IEnumerator Fadein()
    {
        Color col = transitionImg.color;
        t = 0f;

        while (transitionImg.color.a != 0)
        {
            col.a = Mathf.Lerp(1, 0, t / fadeoutTime);
            transitionImg.color = col;

            t += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator PlayTransition()
    {
        Color col = transitionImg.color;
        t = 0f;

        while (transitionImg.color.a != 1)
        {
            col.a = Mathf.Lerp(0, 1, t / fadeoutTime);
            transitionImg.color = col;

            t += Time.deltaTime;

            yield return null;
        }

        trigger.ChangeScene();
    }
}
