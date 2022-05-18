using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject enemyStats;
    public int currentHealth;

    [SerializeField]
    public GameObject player;

    // Trying a ray check, still usefull to avoid enemies following through walls, but I want to test other movement methods to keep physics and collisions
    Ray2D playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
               
        StartCoroutine(ChasePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
    }

     #region Damage taking or dealing

    // Enemy takes damage
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        GetComponentInChildren<HealthBar>().SetHealth(currentHealth);
        StartCoroutine(DamageTaken());
    }

    // Enemy dies function
    void Die()
    {
        Destroy(gameObject, 1.0f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    // Courutine changes colour briefly to show that damage was taken
    IEnumerator DamageTaken()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0.7098039f, 1f);
    }

    #endregion

     #region Chasing
    // Courutine to chase player
    IEnumerator ChasePlayer()
    {
        while (Vector2.Distance(transform.position, player.transform.position) > enemyStats.Range)
        {
            // Move towards target
            Vector2 destination = Vector2.MoveTowards(transform.position, player.transform.position, (enemyStats.Speed / 2) * Time.fixedDeltaTime);
            transform.position = destination;

            yield return null;
        }
        StartCoroutine(CatchedPlayer());
    }

    // Courutine to run when in range
    IEnumerator CatchedPlayer()
    {
        yield return new WaitForSeconds(enemyStats.chaseTimer);
        StartCoroutine(ChasePlayer());
    }
    #endregion

    // Show range in editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)this.transform.position, enemyStats.Range);
    }

    public void HealthSetting()
    {
        currentHealth = enemyStats.Health;
        GetComponentInChildren<HealthBar>().SetMaxHealth(enemyStats.Health);
    }

    /// INFO 
    // Rotate to look at target, LookAt doesnt work on 2D, makes sprite dissapear 
    // transform.LookAt(player.transform);
}
