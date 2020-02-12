using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDir;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 20f;

    [Space] [Header("GroundCheck")] 
    [SerializeField] private Vector2 downOffset = new Vector2(0f,-0.25f);
    [SerializeField] private float collisionRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    
    [Space]
    [Header("States")]
    //[SerializeField] private bool isJumping = false;
    [SerializeField] private bool isFrozen = false;
    [SerializeField] private bool isGrounded = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        moveDir = new Vector2(x, rb.velocity.y);
        
    }
    
    private void FixedUpdate()
    {
        CheckGround();
        Walk(moveDir);
        Jump();
    }
    
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + downOffset, collisionRadius, groundLayer);
    }
    private void Jump()
    {
        if (!isGrounded) return;
        
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }


    private void Walk(Vector2 moveDirection)
    {
        if (isFrozen) return;
        
        rb.velocity = new Vector2(moveDirection.x * moveSpeed,rb.velocity.y);
        
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     print(other.name);
    // }
}