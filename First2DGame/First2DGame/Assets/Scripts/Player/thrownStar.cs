using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrownStar : MonoBehaviour
{
    // Effect of star hitting
    // GameObject hitEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null)
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
        
        if(collision.gameObject.name != "Player")
        { 
        Destroy(gameObject);
        }
    }
}
