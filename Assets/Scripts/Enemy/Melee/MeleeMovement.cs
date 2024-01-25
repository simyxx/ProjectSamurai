using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] float moveSpeed;
    private Rigidbody2D npc;
    private SpriteRenderer sprite;

    void Start()
    {
        npc = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (OnSamePlatform())
        {
            if (player.transform.position.x > npc.transform.position.x)
            {
                npc.velocity = new Vector2 (moveSpeed, npc.velocity.y);
                sprite.flipX = false;
            }
            else if (player.transform.position.x < npc.transform.position.x) 
            {
                npc.velocity = new Vector2 (moveSpeed * (-1f), npc.velocity.y);
                sprite.flipX = true;
            }
        }
        else 
        {
            // zjisti na ktery platforme je hrac a dostane se tam
        }

    }

    bool OnSamePlatform()
    {
        if (npc.transform.position.y - player.transform.position.y < .2f)
        {
            return true;
        }
        return false;
    }
}
