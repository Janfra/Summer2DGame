using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    PlayerHealth_SO playerHealth;

    [field: SerializeField]
    GameObject playerPrefab;
    [field: SerializeField]
    Vector2 spawnPoint;

    // HP
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Setting Health Bar
        currentHealth = playerHealth.Health;
    }

    public void HealthSetting()
    {
        GetComponentInChildren<HealthBar>().SetMaxHealth(playerHealth.Health);
    }

    public void DamageTaken(int damage)
    {
        currentHealth -= damage;
        GetComponentInChildren<HealthBar>().SetHealth(currentHealth);
        StartCoroutine(Damaged());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator Damaged()
    {
        GetComponent<SpriteRenderer>().color = new Color(250, 110, 201);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void Die()
    {
        Debug.Log("Player Died");
    }
}
