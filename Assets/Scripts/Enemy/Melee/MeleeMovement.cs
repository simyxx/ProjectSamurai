using UnityEngine;
using System;

public class MeleeMovement : MonoBehaviour
{
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    private  Rigidbody2D player;
    private Rigidbody2D npc;
    private Animator animator;
    private SpriteRenderer sprite;
    private float npcPlatform;
    private float playerPlatform;
    private float playerY;
    private float npcY;

    void Start()
    {
        npc = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
{
    playerY = player.position.y;
    npcY = npc.position.y;

    UpdateAnimState();

    if (OnSamePlatform())
    {
        if (player.transform.position.x > npc.transform.position.x && IsGrounded())
        {
            npc.velocity = new Vector2(moveSpeed, npc.velocity.y);
            sprite.flipX = false;
        }
        else if (player.transform.position.x < npc.transform.position.x && IsGrounded())
        {
            npc.velocity = new Vector2(moveSpeed * (-1f), npc.velocity.y);
            sprite.flipX = true;
        }
    }
    else
    {
        if (npc.position.y < -1.44f)
        {
            npc.velocity = new Vector2(moveSpeed * (-1f), npc.velocity.y);
            sprite.flipX = true;
        }
        else if (npc.position.y < 0.8f)
        {
            if (playerY > npcY)
            {
                npc.velocity = new Vector2(moveSpeed, npc.velocity.y);
                sprite.flipX = false;
            }
            else if (playerY < npcY && IsGrounded())
            {
                npc.velocity = new Vector2(-moveSpeed, npc.velocity.y);
                sprite.flipX = true;
            }
            else
            {
                npc.velocity = new Vector2(moveSpeed, npc.velocity.y);
                sprite.flipX = false;
            }
        }
        else if (npc.position.y < 2.5f && IsGrounded())
        {
            npc.velocity = new Vector2(moveSpeed, npc.velocity.y);
        }
    }
}

void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("JumpRegister") && playerY > npcY + 0.1f) // skok z platforem na kopce
    {
        npc.velocity = new Vector2(0f, jumpHeight);
    }
    else if (collision.CompareTag("JumpRegisterR") && playerY > npcY && IsGrounded()) // skok z praveho kopce na platformu
    {
        npc.velocity = new Vector2(-moveSpeed, jumpHeight + 1f);
        sprite.flipX = false;
    }
    else if (collision.CompareTag("JumpRegisterM") && playerY < npcY && IsGrounded()) // skok z kopce doleva na stredni nebo dolni kopce
    {
        if (npc.velocity.x > 0f) // Zkontroluj, zda NPC skáče doprava
        {
            npc.velocity = new Vector2(0f, npc.velocity.y); // Zastav pohyb v x-ovém směru
        }
        else
        {
            npc.velocity = new Vector2(-moveSpeed, npc.velocity.y);
            sprite.flipX = true;
        }
    }
}

    void UpdateAnimState()
    {

        int state = 0;
        if (npc.velocity.x > 0f)
        {
            sprite.flipX = false;
        }
        else if (npc.velocity.x < 0f)
        {
            sprite.flipX = true;
        }


        if (npc.velocity.y > .1f)
        {
            state = 1;
        }
        else if (npc.velocity.y < -.1f)
        {
            state = 2;
        }

        animator.SetInteger("state", state);
    }
    

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    bool OnSamePlatform()
    {
        npcPlatform = GetPlatformOfObject(npc.transform.position.y);
        playerPlatform = GetPlatformOfObject(player.transform.position.y);
        if (playerPlatform == npcPlatform)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    int GetPlatformOfObject(float yPositionOfObject)
    {
        if (yPositionOfObject < -1f)
        {
            return 0; // bot
        }
        else if (yPositionOfObject < 0f)
        {
            return 1; // mid
        }
        else 
        {
            return 2; // top
        }      
    }

    public void StopSpawn()
    {
        animator.SetBool("spawn", true);
    }
}
