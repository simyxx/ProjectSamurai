using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject melee;
    [SerializeField] GameObject archer;
    private float timer;
    private bool afterMelee;

    void Start()
    {
        timer = 0;
        afterMelee = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > 8 && !afterMelee)
        {
            afterMelee = true;
            SpawnMelee();
        }

        if (timer >= 12)
        {
            SpawnArcher();
            timer = 0;
            afterMelee = false;
        }
    }

    public void SpawnMelee()
    {
        int platform = Random.Range(1, 4);
        Vector3 spawn = Vector3.zero;
        switch (platform)
        {
            case 1: {
                spawn = new Vector3(Random.Range(10.5f, 16f), -1.9f, 0f);
            } break;
            case 2: {
                spawn = new Vector3(Random.Range(10.5f, 16f), -0.5f, 0f);
            } break;
            case 3: {
                spawn = new Vector3(Random.Range(10.5f, 14.8f), 1.2f, 0f);
            } break;
        }
        Debug.Log("spawn");
        Instantiate(melee, spawn, Quaternion.identity);
    }
    
    public void SpawnArcher()
    {
        
    }
}
