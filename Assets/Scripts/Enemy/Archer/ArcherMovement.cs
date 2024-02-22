using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isWalking;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 1.5f)
        {
            if (rb.position.x > 12f)
            {
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            }
            else 
            {
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
                rb.transform.Rotate(0, 180, 0);
            }
        }
        else 
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("onPosition", true);
            animator.SetBool("shoot", true);
        }
    }

}
