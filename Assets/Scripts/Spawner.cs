using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    private float timeTillSpawn;
    public int spawnCount;
    private float enemyTwoSpawnInterval;
    private float enemyThreeSpawnInterval;
    private float enemyFourSpawnInterval;

    private float randomSpawnBottomBound;
    private float randomSpawnTopBound;


    void Start()
    {

        timeTillSpawn = Random.Range(10f, 30f);
        spawnCount = 0;
        enemyTwoSpawnInterval = 3;
        enemyThreeSpawnInterval = 7;
        enemyFourSpawnInterval = 15;

        randomSpawnBottomBound = 20f;
        randomSpawnTopBound = 25f;


    }

    void Update()
    {
        if(timeTillSpawn <= 0)
        {
            spawnCount++;
            if (spawnCount % enemyFourSpawnInterval == 0f)
            {
                Instantiate(enemy4, transform.position, transform.rotation);
            }
            else if (spawnCount % enemyThreeSpawnInterval == 0f)
            {
                Instantiate(enemy3, transform.position, transform.rotation);
            }
            else if(spawnCount % enemyTwoSpawnInterval == 0f)
            {
                Instantiate(enemy2, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(enemy, transform.position, transform.rotation);
            }
            timeTillSpawn = Random.Range(randomSpawnBottomBound, randomSpawnTopBound);
            randomSpawnBottomBound -= 1f;
            randomSpawnTopBound -= 1f;
            if(randomSpawnBottomBound <= 0f)
            {
                timeTillSpawn = 7f;
            }

        }
        timeTillSpawn -= Time.deltaTime;
    }
}
