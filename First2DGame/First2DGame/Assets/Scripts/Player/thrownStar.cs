using UnityEngine;

public class thrownStar : MonoBehaviour
{
    PlayerShooting giveRB;
    // Effect of star hitting
    // GameObject hitEffect;
    private void Awake()
    {
        giveRB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        giveRB.BulletStore(this.GetComponent<Rigidbody2D>());
    }

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
