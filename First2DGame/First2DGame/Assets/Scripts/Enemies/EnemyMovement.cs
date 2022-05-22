using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject enemyStats;
    [SerializeField]
    private Transform player;
    public Transform playerGet { get => player; }
    [SerializeField]
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Awake()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        StartCoroutine(ChasePlayer());
    }

    #region Chasing
    // Courutine to chase player
    IEnumerator ChasePlayer()
    {
        while (Vector2.Distance(transform.position, player.position) > enemyStats.RangeToWait)
        {
            // Move towards target
            Vector2 destination = Vector2.MoveTowards(transform.position, player.position, enemyStats.Speed * Time.fixedDeltaTime);
            rb.MovePosition(destination);

            yield return null;
        }
        StartCoroutine(AreaOfAttack());
    }

    // Courutine to run when in range
    IEnumerator AreaOfAttack()
    {
        yield return new WaitForSeconds(enemyStats.chaseTimer);
        StartCoroutine(ChasePlayer());
    }
    #endregion

    // Show range in editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)this.transform.position, enemyStats.RangeToWait);
    }

    /// INFO 
    // Rotate to look at target, LookAt doesnt work on 2D, makes sprite dissapear 
    // transform.LookAt(player.transform);

    //// Trying a ray check, still usefull to avoid enemies following through walls, but I want to test other movement methods to keep physics and collisions
    //Ray2D playerDistance;
}
