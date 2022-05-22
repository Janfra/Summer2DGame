using UnityEngine;
using System.Collections;

public class EnemyMeleeDamage : MonoBehaviour
{
    [SerializeField]
    EnemyScriptableObject attackStats;
    SpriteRenderer displayAreaAttack;
    Health playerHealth;
    bool attack = false;
    bool setTimerOff = false;
    float timer = 0.0f;

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
                playerHealth.DamageTaken(attackStats.Damage);
                attack = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            displayAreaAttack.enabled = false;
            setTimerOff = false;
            timer = 0.0f;
            attack = false;
        }
    }
    private void Update()
    {
        if (setTimerOff)
        {
            if(timer < attackStats.attackTimer)
            {
                timer += Time.deltaTime;
            } 
            else
            {
                attack = true;
                timer = 0.0f;
                setTimerOff = false;
            }
        }
    }
}
