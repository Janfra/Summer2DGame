using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field: SerializeField]
    GameObject[] enemies;
    [field: SerializeField]
    GameObject spawnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies[0] == null)
        {
            GameObject newEnemy = Instantiate(spawnEnemy, gameObject.transform.position, gameObject.transform.rotation);
            enemies[0] = newEnemy;
        }
    }
}
