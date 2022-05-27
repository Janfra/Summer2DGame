using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    // Store enemy info to do the check if they are still alive
    [field: SerializeField]
    GameObject[] enemies;

    // Access the pool
    ObjectPooling objectPooler;

    // Type of enemy to decide which pool to access
    [field: SerializeField]
    private string spawnerType;

    // Enemies amount check
    [field: SerializeField]
    private int totalEnemies;
    int poolSize;

    // Respawn check
    [SerializeField]
    private float timePerSpawn = 1.0f;
    [SerializeField]
    private float timePerCheck = 5.0f;
    bool allEnemiesDead = true;
    bool checking = false;

    [System.Serializable]
    enum EnemiesAvailable : int
    {
      Slime = 0,
    }

    // On start check the enemy type, store the pool to access it, get the pool size, create an array of the size of the pool, and then clamp the total enemies to not exceed the pool size.
    void Start()
    {
        // Information storing
        if (spawnerType == null)
            spawnerType = "Slime";
        

        objectPooler = ObjectPooling.Instance;

        // Size checking and setting
        poolSize = objectPooler.GetPoolSize(spawnerType);
        enemies = new GameObject[poolSize];
        totalEnemies = Mathf.Clamp(totalEnemies, 1, poolSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (allEnemiesDead)
        {
            StartCoroutine(Spawn());
            allEnemiesDead = false;
        }
    }

    private void LateUpdate()
    {
        if (!allEnemiesDead && !checking)
        {
            checking = true;
            StartCoroutine(CheckDelay());
        }
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            enemies[i] = objectPooler.SpawnFromPool(spawnerType, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timePerSpawn);
        }
    }

    void EnemiesDeadCheck()
    {
        int deadEnemies = 0;
        for(int i = 0; i < totalEnemies; i++)
        {
            if(enemies[i].activeSelf == false)
            {
                deadEnemies++;
            }
        }
        if(deadEnemies == totalEnemies)
        {
            allEnemiesDead = true;
        }
    }

    IEnumerator CheckDelay()
    {
        yield return new WaitForSeconds(timePerCheck);
        EnemiesDeadCheck();
        checking = false;
        Debug.Log("Check for enemies dead done!");
    }
}
