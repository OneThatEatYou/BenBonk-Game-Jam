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

        if (clip)
        {
            AudioManager.PlayClipAtPoint(clip, transform.position);
        }

        Destroy(gameObject);
    }
}
