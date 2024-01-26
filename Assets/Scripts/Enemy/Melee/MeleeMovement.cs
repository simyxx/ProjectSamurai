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

    void Start()
    {
        npc = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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
            if (npcPlatform == 0)
            {
                npc.velocity = new Vector2(moveSpeed * (-1f), npc.velocity.y);
                sprite.flipX = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JumpRegister"))
        {
            npc.velocity = new Vector2(npc.velocity.x, jumpHeight);
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

    float GetYRangeBetweenCharacters()
    {
        float playerY = Math.Abs(player.transform.position.y);
        float npcY = Math.Abs(npc.transform.position.y);
        if (playerY > npcY) 
        {
            return playerY - npcY;
        }
        else
        {
            return npcY - playerY;
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
