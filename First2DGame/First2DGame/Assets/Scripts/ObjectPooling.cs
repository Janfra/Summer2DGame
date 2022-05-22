using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

     #region Singlenton 
    public static ObjectPooling Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;

    [SerializeField]
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        Debug.Log("Spawning");
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag: " + tag + " does not exist");
            return null;
        } 
        else if (poolDictionary.Count == 0)
        {
            Debug.Log("Emtpy pool");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if(objectToSpawn == null)
        {
            Debug.Log("Object from Dictionary is null");
        }

        poolDictionary[tag].Enqueue(objectToSpawn);
        Debug.Log("Queued");

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        return objectToSpawn;
    }
}
