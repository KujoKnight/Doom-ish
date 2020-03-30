using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyGOs;
    public float radius;
    public float spawnRate = 1f;
    public int spawnLimit;
    public int enemyCount;
    private float spawnCounter;

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            int rand = Random.Range(0, enemyGOs.Length);
            Instantiate(enemyGOs[rand], transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius),0), transform.rotation);
            spawnCounter = spawnRate;
            enemyCount++;
            if(enemyCount >= spawnLimit)
            {
                Destroy(gameObject);
            }
        }
    }
}
