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

        if (Input.GetKeyDown(KeyCode.Mouse0))
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("Enemy zemřel");
        }
    }
    
}
