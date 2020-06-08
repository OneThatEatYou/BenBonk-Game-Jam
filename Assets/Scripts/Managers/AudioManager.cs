using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup sfxMixer;

    #region Singleton

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public AudioSource PlayClipAtPoint(AudioClip clip, Vector2 pos)
    {
        GameObject obj = new GameObject("TempAudio");
        obj.transform.position = pos;
        AudioSource aSource = obj.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.outputAudioMixerGroup = sfxMixer;
        aSource.Play();

        Destroy(obj, clip.length);

        return aSource;
    }
}
