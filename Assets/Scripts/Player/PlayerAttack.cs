using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject meleeAttackHitbox;
    private bool playerIsFacingRight = true;
    private Vector2 hitboxLocalPosition;
    private Vector2 attackDirection;
    private bool isAttacking = false; // Nově přidaná proměnná pro sledování stavu útoku

    void Start()
    {
        hitboxLocalPosition = meleeAttackHitbox.transform.localPosition;
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
        isAttacking = true; // Nastavení stavu útoku na true

        // Spustit animaci změny pozice hitboxu nebo další akce pro útok

        Invoke("DeactivateHitbox", 0.5f);
    }

    void DeactivateHitbox()
    {
        meleeAttackHitbox.SetActive(false);
        isAttacking = false; // Nastavení stavu útoku na false po deaktivaci hitboxu
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("Enemy zemřel");
        }
    }
}
