using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field: SerializeField]
    GameObject[] enemies;
    ObjectPooling objectPooler;

    [field: SerializeField]
    public string spawnerType;
    [field: SerializeField]
    int totalEnemies;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnerType == null)
            spawnerType = "DefaultEnemy";
        objectPooler = ObjectPooling.Instance;
        for(int i = 0; i < totalEnemies; i++)
        {
            enemies[i] = objectPooler.SpawnFromPool(spawnerType, transform.position, Quaternion.identity);
            enemies[i].GetComponent<Enemy>().AssignType(spawnerType);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
