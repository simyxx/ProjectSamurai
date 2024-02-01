using UnityEngine;
using System;

public class MeleeMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    private Rigidbody2D npc;
    private SpriteRenderer sprite;
    private float npcPlatform;
    private float playerPlatform;
    private float playerY;
    private float npcY;

    void Start()
    {
        npc = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        playerY = player.position.y;
        npcY = npc.position.y;

        if (OnSamePlatform())
        {
            if (player.transform.position.x > npc.transform.position.x && IsGrounded())
            {
                npc.velocity = new Vector2 (moveSpeed, npc.velocity.y);
                sprite.flipX = false;
            }
            else if (player.transform.position.x < npc.transform.position.x && IsGrounded()) 
            {
                npc.velocity = new Vector2 (moveSpeed * (-1f), npc.velocity.y);
                sprite.flipX = true;
            }
        }
        else 
        {
            if (npc.position.y < -1.44f) // npc je na spodni platforme, hrac muze byt jen vys 
            {
                npc.velocity = new Vector2(moveSpeed * (-1f), npc.velocity.y);
                sprite.flipX = true; 
            }
            else if (npc.position.y < 0.8f) // npc je na prostredni platforme
            {
                if (playerY > npcY) // hrac je vys nez npc
                {
                    npc.velocity = new Vector2(moveSpeed, npc.velocity.y);
                    sprite.flipX = false;
                }
                else 
                {
                    npc.velocity = new Vector2(-moveSpeed, npc.velocity.y);
                    sprite.flipX = true;
                }
            }
            else if (npc.position.y < 2.5f)
            {
                npc.velocity = new Vector2(moveSpeed, npc.velocity.y);
            }
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("JumpRegisterM") && playerY < npcY)
        {
            npc.AddForce(new Vector2(moveSpeed * 999999993f, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JumpRegister") && playerY > npcY) // skok z platforem na kopce
        {
            npc.velocity = new Vector2(npc.velocity.x, jumpHeight);
        }
        if (collision.CompareTag("JumpRegisterR")) // skok z praveho kopce na platformu
        {
            if (playerY < npcY)
            {
                npc.velocity = new Vector2(-moveSpeed, jumpHeight);
                sprite.flipX = true;
            }
            else 
            {
                npc.velocity = new Vector2(-moveSpeed, jumpHeight + 1f);
                sprite.flipX = false;
            }
        }
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
}
