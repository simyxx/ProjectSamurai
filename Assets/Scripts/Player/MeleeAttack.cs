using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Zde přidejte kód pro zranění nebo zničení nepřítele
            Destroy(other.gameObject);
            Debug.Log("Enemy zemřel");
        }
    }
}
