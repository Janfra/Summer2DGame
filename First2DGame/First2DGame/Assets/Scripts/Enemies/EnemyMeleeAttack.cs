using UnityEngine;
using System.Collections;

public class EnemyMeleeAttack : EnemyAttack
{
    SpriteRenderer displayAreaAttack;
    Health playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        displayAreaAttack = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            displayAreaAttack.enabled = true;
            if (!attack)
            {
                setTimerOff = true;
            } 
            else if (attack)
            {
                playerHealth.DamageTaken(enemyStats.Damage);
                attack = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            displayAreaAttack.enabled = false;
            setTimerOff = false;
            timer = 0.0f;
            attack = false;
        }
    }
}
