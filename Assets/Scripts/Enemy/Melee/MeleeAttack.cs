using System;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private GameObject meleeAttackHitbox;
    private  Rigidbody2D player;
    private Animator playerAnim;
    private Rigidbody2D npc;
    private Animator animator;
    private Vector2 hitboxLocalPosition;
    private float rangeBtwCharactersX;
    float npcPlatform;
    float playerPlatform;
    private bool isAttacking = false;

    void Start()
    {
        meleeAttackHitbox = GameObject.Find("MeleeAttack");
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        npc = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitboxLocalPosition = meleeAttackHitbox.transform.position;
    }

    void Update()
    {
        rangeBtwCharactersX = GetXRangeBetweenCharacters();

        MoveMeleeAttackHitbox();

        if (rangeBtwCharactersX < .38f && OnSamePlatform())
        {
            Attack();
        }
    }

    void MoveMeleeAttackHitbox()
    {
        if (player.transform.position.x > npc.transform.position.x)
        {
            meleeAttackHitbox.transform.position = new Vector3(hitboxLocalPosition.x, hitboxLocalPosition.y, 0);
        }
        else if (player.transform.position.x < npc.transform.position.x)
        {
            meleeAttackHitbox.transform.position = new Vector3(-hitboxLocalPosition.x, hitboxLocalPosition.y, 0);
        }
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

    float GetXRangeBetweenCharacters()
    {
        float playerX = player.transform.position.x;
        float npcX = npc.transform.position.x;
        if (playerX > npcX) 
        {
            return playerX - npcX;
        }
        else
        {
            return npcX - playerX;
        } 
    }

    int GetPlatformOfObject(float yPositionOfObject)
    {
        if (yPositionOfObject < -1.68f)
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

    void Attack()
    {
        meleeAttackHitbox.SetActive(true);
        isAttacking = true; 
        animator.SetBool("attack", true);
        Invoke("DeactivateHitbox", 0.27f);
    }

    void DeactivateHitbox()
    {
        meleeAttackHitbox.SetActive(false);
        isAttacking = false; 
        animator.SetBool("attack", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttacking && collision.collider.CompareTag("Player"))
        {  
            // idealne misto zniceni jen vsechno pozastavit / prehrat animaci smrti a pozastavit vse
            playerAnim.SetTrigger("death");
   
            //Destroy(collision.gameObject);
            Debug.Log("Hráč zemřel");
        }
    }
}
