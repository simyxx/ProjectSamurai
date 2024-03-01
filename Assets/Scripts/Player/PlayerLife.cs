using Unity.VisualScripting;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public static int hp;
    private Animator anim;

    void Start()
    {
        hp = 3;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (hp == 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            hp = 3;
        }
    }

    void Die()
    {
        anim.SetTrigger("death");
        Destroy(gameObject, 1f);
        SaveData data = new SaveData();
        if (PlayerScore.totalScore > data.highscore)
        {
            data.highscore = PlayerScore.totalScore;
            SaveManager saveManager = new SaveManager();
            saveManager.SaveGame();
        }
    }
}
