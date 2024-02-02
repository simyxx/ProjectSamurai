using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLife : MonoBehaviour
{
    void OnDeathAnimEnd()
    {
        Destroy(gameObject, 0.1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            Destroy(collision.gameObject);
        }
    }
}