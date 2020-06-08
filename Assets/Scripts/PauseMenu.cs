using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;
    float bgmVol;
    float sfxVol;

    public AudioSource source;
    public AudioClip[] sfxClips;

    private void Start()
    {
        mainMixer.GetFloat("BGMVolume", out bgmVol);
        mainMixer.GetFloat("SFXVolume", out sfxVol);
        bgmSlider.value = bgmVol;
        sfxSlider.value = sfxVol;
    }

    public void ChangeBGMVolume(float volume)
    {
        mainMixer.SetFloat("BGMVolume", volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVolume", volume);
    }

    public void PlayRandomSFX()
    {
        source.clip = sfxClips[Random.Range(0, sfxClips.Length)];
        source.Play();
    }
}
