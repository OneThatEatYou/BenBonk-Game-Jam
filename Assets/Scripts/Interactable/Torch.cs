using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactable
{
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

        isThrown = false;

        HandTorch obj = Instantiate(handTorch, PlayerManager.hand).GetComponent<HandTorch>();
        obj.torchStartPos = torchStartPos;

        Debug.Log(torchStartPos);

        Destroy(gameObject);
    }
}
