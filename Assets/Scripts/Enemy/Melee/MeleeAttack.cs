using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] GameObject meleeAttackHitbox;
    [SerializeField] Rigidbody2D player;
    private Rigidbody2D npc;
    private Animator animator;
    private float rangeBtwCharacters;
    private Vector2 hitboxLocalPosition;
    private Vector2 attackDirection;
    private bool isAttacking = false;
    private bool npcIsFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        npc = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitboxLocalPosition = meleeAttackHitbox.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rangeBtwCharacters = Math.Abs(player.transform.position.x - npc.transform.position.x);

        if (player.transform.position.x > npc.transform.position.x)
        {
            npcIsFacingRight = true;
        }
        else if (npc.transform.position.x < player.transform.position.x)
        {
            npcIsFacingRight = false;
        }

        attackDirection = npcIsFacingRight ? Vector2.right : Vector2.left;

        if (rangeBtwCharacters < .3f)
        {
            Attack();
        }
    }

    void LateUpdate()
    {
        meleeAttackHitbox.transform.localPosition = new Vector3(attackDirection.x * hitboxLocalPosition.x, hitboxLocalPosition.y, 0);
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
            Destroy(collision.gameObject);
            Debug.Log("Hráč zemřel");
        }
    }
}
