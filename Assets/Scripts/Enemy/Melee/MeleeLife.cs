using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            Destroy(collision.gameObject);
        }
    }
}