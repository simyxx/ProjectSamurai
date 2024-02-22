using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject melee;
    [SerializeField] GameObject archer;
    private float timer;
    private bool afterMelee;

    public static EnemySpawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
        Instantiate(melee, spawn, Quaternion.identity);

    }
    
    public void SpawnArcher()
    {
        int platform = Random.Range(1, 3);
        Vector3 spawn = Vector3.zero;
        if (platform == 1)
        {
            spawn = new Vector3(Random.Range(22f, 22.75f), 0.48f, 0f);
        }
        else 
        {
            spawn = new Vector3(Random.Range(3f, 3.75f), 0f, 0f);
        }
        Instantiate(archer, spawn, Quaternion.identity);
    }
}
