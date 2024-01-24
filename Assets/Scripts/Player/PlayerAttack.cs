using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject meleeAttackHitbox;
    [SerializeField] private Rigidbody2D rb;
    private Animator animator;
    private bool playerIsFacingRight = true;
    private Vector2 hitboxLocalPosition;
    private Vector2 attackDirection;
    private bool isAttacking = false;

    void Start()
    {
        hitboxLocalPosition = meleeAttackHitbox.transform.localPosition;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            playerIsFacingRight = true;
        }
        else if (horizontalInput < 0)
        {
            playerIsFacingRight = false;
        }

        attackDirection = playerIsFacingRight ? Vector2.right : Vector2.left;

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.LeftControl))
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
        if (isAttacking && collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Enemy zemřel");
        }
    }
    
}
