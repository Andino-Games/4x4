using UnityEngine;

public class HealthBar : MonoBehaviour
{
  [SerializeField]private float maxHealth = 100f;
  [SerializeField]private float currentHealth;

    private void OnEnable()
    {
        EventManager.Instance.OnDamageTaken += TakeDamage;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDamageTaken -= TakeDamage;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Player Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        // Add death logic here (e.g., respawn, game over screen)
    }

   
}
