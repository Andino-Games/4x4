using UnityEngine;

public class Health : MonoBehaviour
{
  public float maxHealth = 100f;
  public float currentHealth;
     

    private void Start()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnDamageTaken += TakeDamage;
        }
        else
        {
            Debug.LogWarning("EventManager.Instance es null en Health OnEnable. No se puede suscribir al evento.");
        }
        currentHealth = maxHealth;
    }
    private void OnDisable()
    {
        EventManager.Instance.OnDamageTaken -= TakeDamage;
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
        Debug.Log("Player has died.");
        //this.gameObject.SetActive(false);
    }

   
}
