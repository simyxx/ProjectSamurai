using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            Die(); 
        }
    }

    void Die()
    {
        Debug.Log("Postava umřela!");
        gameObject.SetActive(false);
    }
}
