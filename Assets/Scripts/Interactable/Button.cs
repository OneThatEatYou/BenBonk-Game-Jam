using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    public Gate[] gate;
    bool canBeOpened;
    int numOfGatesOpened;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip clip;

    [Space]

    public UnityEvent onInteraction;

    public override void Use()
    {
        base.Use();

        for (int i = 0; i < gate.Length; i++)
        {
            canBeOpened = gate[i].Open();
            //Debug.Log(canBeOpened);

            if (canBeOpened)
            {
                numOfGatesOpened++;
                canBeOpened = false;
            }
        }

        if (numOfGatesOpened > 0)
        {
            PlayAudio();
            numOfGatesOpened = 0;
        }

        onInteraction.Invoke();
    }

    void PlayAudio()
    {
        source.clip = clip;
        source.Play();
    }
}
