using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool staysOpen = true;
    public float timeBeforeClosing;

    SpriteRenderer spriteRenderer;
    Sprite curSprite;
    Collider2D col;

    bool isOpened = false;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        curSprite = spriteRenderer.sprite;
        col = GetComponent<Collider2D>();
    }

    public void Open()
    {
        if (isOpened)
            return;

        spriteRenderer.sprite = null;
        col.enabled = false;

        isOpened = true;

        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(timeBeforeClosing);

        spriteRenderer.sprite = curSprite;
        col.enabled = true;

        isOpened = false;
    }
}
