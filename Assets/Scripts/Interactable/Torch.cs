using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactable
{
    [Header("Torch Settings")]
    public GameObject handTorch;

    [HideInInspector]
    public Vector2 torchStartPos;

    [HideInInspector]
    public bool isThrown = false;

    public AudioClip pickupClip;
    public AudioClip collisionClip;

    bool hadCollidedRecently;

    private void Start()
    {
        if (!isThrown)
        {
            torchStartPos = transform.position;
        }
    }

    public override void Use()
    {
        base.Use();

        if (GameObject.FindGameObjectWithTag("HandTorch"))
        {
            return;
        }

        HandTorch obj = Instantiate(handTorch, PlayerManager.hand).GetComponent<HandTorch>();
        obj.torchStartPos = torchStartPos;

        //Debug.Log(torchStartPos);

        if (pickupClip)
        {
            AudioManager.instance.PlayClipAtPoint(pickupClip, transform.position);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionClip && !hadCollidedRecently)
        {
            AudioSource source = AudioManager.instance.PlayClipAtPoint(collisionClip, transform.position);
            source.spatialBlend = 0.9f;
            //source.rolloffMode = AudioRolloffMode.Linear;
            source.minDistance = 2f;
            source.maxDistance = 10f;
            source.volume = 0.8f;
            source.pitch = 0.6f;

            hadCollidedRecently = true;
            Invoke("ResetCollided", 0.3f);
        }
    }

    void ResetCollided()
    {
        hadCollidedRecently = false;
    }
}
