using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public void PlaySFX()
    {
        source.clip = clip;
        source.Play();
    }
}
