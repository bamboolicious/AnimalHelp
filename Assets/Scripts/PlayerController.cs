using System;
using Managers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    private Vector2 moveDir;
    private Vector2 startPos;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 20f;

    [Space] [Header("GroundCheck")] 
    [SerializeField] private Vector2 downOffset = new Vector2(0f,-0.25f);
    [SerializeField] private float collisionRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    
    [Space]
    [Header("States")]
    public bool isGrounded = false;
    public bool jumped = false;
    

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        //GameController.OnQuestionChange += ResetPlayerPos;
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        moveDir = new Vector2(x, rb.velocity.y);
        
    }
    
    private void FixedUpdate()
    {
        if (GameController.isDead) return;
        CheckGround();
        Walk(moveDir);
        Jump();
    }

    private void ResetPlayerPos()
    {
        transform.position = startPos;
        rb.velocity = Vector2.zero;
    }
    
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + downOffset, collisionRadius, groundLayer);
        if (isGrounded) jumped = false;
    }
    private void Jump()
    {
        if (!isGrounded || jumped) return;
        
        if (Input.GetButton("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
            jumped = true;
        }
    }


    private void Walk(Vector2 moveDirection)
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

}