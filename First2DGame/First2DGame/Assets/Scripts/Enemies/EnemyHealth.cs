using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    EnemyHealth_SO totalHealth;
    [SerializeField]
    HealthBar healthDisplay;
    public int currentHealth;
    SpriteRenderer enemyColour;

    // Start is called before the first frame update
    void Awake()
    {
        if (totalHealth == null)
            totalHealth = (EnemyHealth_SO)ScriptableObject.CreateInstance(typeof(EnemyHealth_SO));
        currentHealth = totalHealth.Health;
        enemyColour = this.GetComponent<SpriteRenderer>();
        healthDisplay = GetComponentInChildren<HealthBar>();
    }

    // Enemy takes damage
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        healthDisplay.SetHealth(currentHealth);
        StartCoroutine(DamageTaken());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Enemy dies function
    void Die()
    {
        enemyColour.color = Color.gray;
        StartCoroutine(Died());
    }

    // Courutine changes colour briefly to show that damage was taken
    IEnumerator DamageTaken()
    {
        enemyColour.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemyColour.color = Color.white;
    }

    IEnumerator Died()
    {
        yield return new WaitForSeconds(0.5f);
        Heal(totalHealth.Health);
        gameObject.SetActive(false);
    }

    public void HealthSetting()
    {
        currentHealth = totalHealth.Health;
        healthDisplay.SetMaxHealth(totalHealth.Health);
    }

    void Heal(int healAmount)
    {
        currentHealth = Mathf.Clamp(healAmount, 0, totalHealth.Health);
        healthDisplay.SetHealth(currentHealth);
    }
}
