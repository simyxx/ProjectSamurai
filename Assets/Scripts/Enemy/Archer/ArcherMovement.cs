using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isWalking;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isWalking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            if (rb.position.x > 15f)
            {
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            }
            else 
            {
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            }
        }
        else 
        {
            animator.SetBool("onPosition", true);
            animator.SetBool("shoot", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ArcherStop"))
        {
            isWalking = false;
        }
    }
}
