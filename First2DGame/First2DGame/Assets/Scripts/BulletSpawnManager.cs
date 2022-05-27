using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class BulletType
    {
        public string bulletType;
        [HideInInspector]
        public int currentBullet = 0;
        int totalBullets;

        public void NextBullet()
        {
            currentBullet++;
            if (currentBullet == totalBullets)
            {
                currentBullet = 0;
            }
        }

        public void SetTotalBullets()
        {
            totalBullets = ObjectPooling.Instance.GetPoolSize(bulletType);
            if(totalBullets == 0)
            {
                Debug.LogError("Bullet Manager Requesting non-existing total bullets");
            }
        }
    }

    public List<BulletType> bulletTypes;

    private void Start()
    {
        foreach(BulletType bulletType in bulletTypes)
        {
            bulletType.SetTotalBullets();
        }
    }

    public int GetCurrentBullet(string bulletTypeName)
    {
        int bulletArrayNumber = BulletTypeCheck(bulletTypeName);

        if (bulletArrayNumber == -1)
        {
            return 0;
        }
        else
        {
            int currentBullet = bulletTypes[bulletArrayNumber].currentBullet;
            bulletTypes[bulletArrayNumber].NextBullet();
            return currentBullet;
        }
    }

    private int BulletTypeCheck(string bulletTypeName)
    {
        for(int i = 0; i < bulletTypes.Count; i++)
        {
            if(bulletTypes[i].bulletType == bulletTypeName)
            {
                return i;
            }
        }
        Debug.LogError("Bullet type given to manager does not exist");
        return -1;
    }
}
