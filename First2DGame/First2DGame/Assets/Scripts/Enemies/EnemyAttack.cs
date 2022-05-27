using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyAttack : MonoBehaviour
{
    // Enemy type stats
    [field: SerializeField]
    protected EnemyScriptableObject enemyStats;

    // Timer 
    protected bool attack = false;
    protected bool setTimerOff = false;
    protected float timer = 0.0f;

    private void Awake()
    {
        if (enemyStats == null)
            enemyStats = (EnemyScriptableObject)ScriptableObject.CreateInstance(typeof(EnemyScriptableObject));
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
                // Set enemy specific code here
                Debug.Log("override OnTriggerStay to set attack");
                attack = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            setTimerOff = false;
            timer = 0.0f;
            attack = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (setTimerOff)
        {
            if (timer < enemyStats.attackTimer)
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
