using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpVel;
    //bool grounded;
    public Vector2 basePos;
    public Vector2 baseSize;
    public LayerMask groundLayer;

    [Header("Action")]
    public Transform handPos;
    public Vector2 interactablePos;
    public float interactableRad;
    public LayerMask interactableLayer;

    Checkpoint latestCheckpoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
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
    }

    void Move(float x)
    {
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        bool grounded = Physics2D.OverlapBox(rb.position + basePos, baseSize, 0, groundLayer);

        if (!grounded)
            return;

        rb.velocity += new Vector2(0, jumpVel);
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
        Debug.Log("Player died. Press 'R' to respawn");
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
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        latestCheckpoint = checkpoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + basePos, baseSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + interactablePos, interactableRad);
    }
}
