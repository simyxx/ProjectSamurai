using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private float horizontal;
    private Animator animator;
    private SpriteRenderer sprite;
    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 9f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public float slopeRayLength = 0.5f;
    private bool isSliding;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded() || Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); 
        }

        
        UpdateAnimState();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void UpdateAnimState()
    {

        MovementState state;
        if (horizontal > 0f){
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (horizontal < 0f){
            state = MovementState.running;
            sprite.flipX = true;
        }
        else {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f){
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f){
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);

    }
    
}
