using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth_SO playerHealth;
    [SerializeField]
    HealthBar healthDisplay;
    SpriteRenderer playerColour;

    // Possibly respawn. Not the best place to do
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Vector2 spawnPoint;

    // HP
    private int currentHealth;

    // Start is called before the first frame update
    void Awake()
    {
        // Setting Health Bar
        currentHealth = playerHealth.Health;
        healthDisplay = GetComponentInChildren<HealthBar>();
        playerColour = GetComponent<SpriteRenderer>();
    }

    public void HealthSetting()
    {
        healthDisplay.SetMaxHealth(playerHealth.Health);
    }

    public void DamageTaken(int damage)
    {
        currentHealth -= damage;
        healthDisplay.SetHealth(currentHealth);
        StartCoroutine(Damaged());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator Damaged()
    {
        playerColour.color = new Color(250, 110, 201);
        yield return new WaitForSeconds(0.2f);
        playerColour.color = Color.red;
    }

    void Die()
    {
        Debug.Log("Player Died");
        Heal(playerHealth.Health);
    }

    void Heal(int healAmount)
    {
        currentHealth = Mathf.Clamp(healAmount, 0, playerHealth.Health);
        healthDisplay.SetHealth(currentHealth);
    }
}
