using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsSpawn : MonoBehaviour
{
    // Bullet Pooling
    protected ObjectPooling objectPooler;
    [field: SerializeField]
    protected Rigidbody2D[] bulletRB;
    [field: SerializeField]
    protected string ammoType;
    protected BulletSpawnManager currentBullet;

    // Shooting
    [field: SerializeField]
    protected Transform firePoint;
    [field: SerializeField]
    protected float throwForce = 10.0f;

    private void Awake()
    {
        objectPooler = ObjectPooling.Instance;
        bulletRB = new Rigidbody2D[objectPooler.GetPoolSize(ammoType)];
        currentBullet = GameObject.Find("BulletManager").GetComponent<BulletSpawnManager>();
    }

    private void Start()
    {
        if (currentBullet == null)
            Debug.LogError("Bullet Manager is empty in " + gameObject.name);
        BulletStoring();
    }

    public void SpawnBullet()
    {
        objectPooler.SpawnFromPool(ammoType, firePoint.position, firePoint.rotation);
        bulletRB[currentBullet.GetCurrentBullet(ammoType)].AddForce(firePoint.up * throwForce, ForceMode2D.Impulse);
    }

    protected void BulletStoring()
    {
        for (int i = 0; i < objectPooler.GetPoolSize(ammoType); i++)
        {
            bulletRB[i] = objectPooler.GetRigidbody(ammoType);
        }
    }

    public string GetAmmoType()
    {
        return ammoType;
    }
}
