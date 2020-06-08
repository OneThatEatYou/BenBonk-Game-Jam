using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpVel;
    public float terminalVel;
    public Vector2 basePos;
    public Vector2 baseSize;
    public LayerMask groundLayer;
    bool grounded = true;
    bool lastGrounded = true;

    [HideInInspector]
    public bool canMove = true;

    int curDir = 1;
    SpriteRenderer sRenderer;

    [Header("Action")]
    public Transform handPos;
    public Vector2 interactablePos;
    public float interactableRad;
    public LayerMask interactableLayer;

    Checkpoint latestCheckpoint;

    Animator anim;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip jumpClip;
    public AudioClip fallClip;
    public AudioClip deathClip;

    [Space]
    public GameObject settingCanvas;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");

        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            anim.SetFloat("xSpeed", 0);
            return;
        }

        Move(xInput);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }

        if (rb.velocity.y < terminalVel)
        {
            rb.velocity = new Vector2(rb.velocity.x, terminalVel);
        }

        anim.SetFloat("xSpeed", xInput);
        anim.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();
    }

    void Move(float x)
    {
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

        if (x < 0 && curDir == 1)
        {
            Flip();
        }
        else if (x > 0 && curDir == -1)
        {
            Flip();
        }
    }

    void Flip()
    {
        sRenderer.flipX = !sRenderer.flipX;
        curDir = -curDir;

        CameraController.instance.FlipCamera(curDir);
    }

    void CheckIfGrounded()
    {
        grounded = Physics2D.OverlapBox(rb.position + basePos, baseSize, 0, groundLayer);

        //check if just grounded
        if (lastGrounded != grounded && grounded && rb.velocity.y < 0.1f)
        {
            PlayAudio(fallClip);
            //Debug.Log(rb.velocity.y);
        }

        lastGrounded = grounded;
    }

    void Jump()
    {
        if (!grounded)
            return;

        anim.SetTrigger("Jump");

        rb.velocity += new Vector2(0, jumpVel);

        PlayAudio(jumpClip);
    }

    void Interact()
    {
        Collider2D obj = Physics2D.OverlapCircle(rb.position + interactablePos, interactableRad, interactableLayer);

        if (obj)
        {
            Interactable inter = obj.GetComponent<Interactable>();
            inter.Use();
        }
        else
        {
            Debug.Log("No interactable object found");
        }
    }

    public void Die()
    {
        //Debug.Log("Player died. Press 'R' to respawn");

        Respawn();
    }

    public void Respawn()
    {
        if (!latestCheckpoint)
        {
            Debug.Log("No checkpoint available");
            return;
        }

        transform.position = latestCheckpoint.spawn.position;

        GameManager.instance.ResetScene();

        AudioManager.instance.PlayClipAtPoint(deathClip, transform.position);
    }

    public bool SetCheckpoint(Checkpoint checkpoint)
    {
        if (latestCheckpoint == checkpoint)
        {
            //Debug.Log("Same checkpoint");
            return true;
        }

        if (latestCheckpoint)
        {
            latestCheckpoint.RemoveCheckpoint();
        }

        latestCheckpoint = checkpoint;

        //Debug.Log("new checkpoint");

        return false;
    }

    void PlayAudio(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    void ToggleSettings()
    {
        settingCanvas.SetActive(!settingCanvas.activeInHierarchy);

        Time.timeScale = 1 - Time.timeScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + basePos, baseSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + interactablePos, interactableRad);
    }
}
