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

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
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


    private int GetPoolArrayNumber(string tag)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].tag == tag)
            {
                return i;
            }
        }
        return -1;
    }

    public int GetPoolSize(string tag)
    {
        int poolArrayNumber = GetPoolArrayNumber(tag);
        if(poolArrayNumber == -1)
        {
            Debug.Log("The tag given: " + tag + " does not exist");
            return 0;
        }
        else
        {
            return pools[poolArrayNumber].size;
        }
    }

    public Rigidbody2D GetRigidbody(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Rigidbody requested a non-existing tag!");
            return null;
        }
        else
        {
            GameObject objToRigidbody = poolDictionary[tag].Dequeue();
            poolDictionary[tag].Enqueue(objToRigidbody);
            if (objToRigidbody == null)
                Debug.LogError("This pool is empty!");
            Rigidbody2D returnRB = objToRigidbody.GetComponent<Rigidbody2D>();
            if (returnRB == null)
                Debug.LogError("Objects from this pool have no rigidbody!");
            return returnRB;
        }
    }
}
