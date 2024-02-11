using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArcherShoot : MonoBehaviour
{
    [SerializeField] Transform arrowPos;
    [SerializeField] GameObject arrow;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 4)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(arrow, arrowPos.position, Quaternion.identity);
    }
}
