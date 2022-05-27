using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Effect of star hitting
    // GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyHealth>() != null)
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        
        if(collision.gameObject.name != "Player")
        { 
        gameObject.SetActive(false);
        }
    }
}
