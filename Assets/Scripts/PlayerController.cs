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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + basePos, baseSize);
    }
}
