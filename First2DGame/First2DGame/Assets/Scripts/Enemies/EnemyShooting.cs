using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : EnemyAttack
{
    CircleCollider2D range;
    BulletsSpawn bulletGeneration;
    FacePlayer facePlayer;

    // Start is called before the first frame update
    void Awake()
    {
        range = this.GetComponent<CircleCollider2D>();
        range.radius = enemyStats.AttackRange;
        bulletGeneration = this.GetComponent<BulletsSpawn>();
        facePlayer = this.GetComponent<FacePlayer>();
    }

    private void Start()
    {
        if (bulletGeneration == null)
            Debug.LogError(gameObject.name + " has no bullet spawn component");
        if (facePlayer == null)
            Debug.LogError(gameObject.name + " has no face player component");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            if (!attack)
            {
                setTimerOff = true;
            }
            else if (attack)
            {
                Debug.Log("Enemy Shooting");
                bulletGeneration.SpawnBullet();
                attack = false;
            }
        }
    }
}
