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

    public static Spawner instance;

    private List<GameObject> goContainer;



    private void Awake()
    {
        //Jenny: Safety optimization, otherwise two game managers would be very hard to control.
        if (instance != null)
        {
            Debug.LogError("Two GameManagers Active!");
            DestroyImmediate(this.gameObject);
        }
        instance = this;
        goContainer = new List<GameObject>();
    }

    private void Start()
    {
        StartCoroutine(SpawnerCycle(spawnRate));
    }

    //IEnum on spawn rate rather than in update loop.
    IEnumerator SpawnerCycle(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            //Check both conditions at once rather than instanciate and destroy afterwards.
            if (enemyCount >= spawnLimit)
            {
                int rand = Random.Range(0, enemyGOs.Length);
                GameObject temp = null;


                if (goContainer.Count != 0)
                {
                    temp = goContainer.Find(x => enemyGOs[rand]);
                    temp.transform.position = transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0f);
                    temp.transform.rotation = transform.rotation;
                    temp.SetActive(true);

                }

                if (temp == null)
                {
                    temp = Instantiate(enemyGOs[rand], transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0), transform.rotation);
                }
                enemyCount++;
            }
        }
    }

    //To be used in enemies.
    public void Dispose(GameObject go)
    {
        goContainer.Add(go);
        go.SetActive(false);
    }

    /*
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
    }*/
}