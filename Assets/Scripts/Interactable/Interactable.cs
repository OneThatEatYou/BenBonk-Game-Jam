using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip clip;

    public virtual void Use()
    {
        Debug.Log("Using " + gameObject.name);
    }
}
