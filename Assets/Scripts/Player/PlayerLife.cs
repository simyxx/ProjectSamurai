using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            Die(); // Zavolání funkce pro zpracování smrti postavy
        }
    }

    void Die()
    {
        // Tady můžete přidat kód, který bude proveden při smrti postavy
        // Například restartování levelu, zobrazení smrti, atd.
        Debug.Log("Postava umřela!");
        
        // Pro ukázku, můžete deaktivovat (skrýt) objekt postavy:
        gameObject.SetActive(false);
    }
}
