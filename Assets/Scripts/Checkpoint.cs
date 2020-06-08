using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawn;
    public GameObject lightSource;
    public GameObject wallLightSource;

    SpriteRenderer spriteRenderer;
    public Sprite onSprite;
    public Sprite offSprite;

    [Space]

    public ParticleSystem particle;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip clip;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool isRepeatedCheckpoint = collision.GetComponent<PlayerController>().SetCheckpoint(this);

            if (!isRepeatedCheckpoint)
            {
                SetCheckpoint(isRepeatedCheckpoint);
            }
        }
    }

    void SetCheckpoint(bool isRepeatedCheckpoint)
    {
        //Debug.Log("Checkpoint set");

        lightSource.SetActive(true);
        wallLightSource.SetActive(true);

        spriteRenderer.sprite = onSprite;

        GameManager.instance.ResetScene();

        SpawnParticle();

        PlayAudio();
    }

    public void RemoveCheckpoint()
    {
        lightSource.SetActive(false);
        wallLightSource.SetActive(false);

        spriteRenderer.sprite = offSprite;
    }

    void SpawnParticle()
    {
        particle.Play();
    }

    void PlayAudio()
    {
        source.clip = clip;
        source.Play();
    }
}
