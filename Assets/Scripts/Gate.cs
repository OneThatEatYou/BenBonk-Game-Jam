using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool staysOpen = true;
    public float timeBeforeClosing;
    public float animTime;

    Rigidbody2D rb;

    bool isOpened = false;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip clip;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Open()
    {
        if (isOpened)
            return;

        isOpened = true;

        //currently playing button sfx
        PlayAudio();

        StartCoroutine(OpenAnimation(animTime));
    }

    void PlayAudio()
    {
        source.clip = clip;
        source.Play();
    }

    IEnumerator OpenAnimation(float time)
    {
        Vector2 startPos = rb.position;
        Vector2 targetPos = rb.position + new Vector2(0, -3.1f);

        float elapsed = 0f;

        while (!Mathf.Approximately(rb.position.y, targetPos.y))
        {
            float y = Mathf.Lerp(startPos.y, targetPos.y, elapsed / time);
            Vector2 pos = new Vector2(rb.position.x, y);
            rb.MovePosition(pos);

            elapsed += Time.deltaTime;

            yield return null;
        }

        rb.MovePosition(targetPos);

        //Debug.Log("Finished lerping");

        if (!staysOpen)
        {
            StartCoroutine(CloseAnimation(time));
        }

    }


    IEnumerator CloseAnimation(float time)
    {
        yield return new WaitForSeconds(timeBeforeClosing);

        Vector2 startPos = rb.position;
        Vector2 targetPos = rb.position + new Vector2(0, 3.1f);

        float elapsed = 0f;

        while (!Mathf.Approximately(rb.position.y, targetPos.y))
        {
            float y = Mathf.Lerp(startPos.y, targetPos.y, elapsed / time);
            Vector2 pos = new Vector2(rb.position.x, y);
            rb.MovePosition(pos);

            elapsed += Time.deltaTime;

            yield return null;
        }

        rb.MovePosition(targetPos);

        isOpened = false;
    }
}
