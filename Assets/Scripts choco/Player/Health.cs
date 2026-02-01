using UnityEngine;

public class Health : MonoBehaviour
{
  public float maxHealth = 100f;
  public float currentHealth;
  public Respawn respawn;  
     

    private void Start()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnDamageTaken += TakeDamage;
            EventManager.Instance.OnMaxHealthIncreased += IncreasedHealth;
        }
        else
        {
            Debug.LogWarning("EventManager.Instance es null en Health OnEnable. No se puede suscribir al evento.");
        }
        currentHealth = maxHealth;
        respawn = GetComponent<Respawn>();
    }
    private void OnDisable()
    {
        EventManager.Instance.OnDamageTaken -= TakeDamage;
        EventManager.Instance.OnMaxHealthIncreased -= IncreasedHealth;
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
        if(respawn.canRespawn == true)
        {
            respawn.RespawnPlayer(this.gameObject);
            currentHealth = maxHealth;
            
        }
        else
        {
            Debug.Log("Player has died.");
            this.gameObject.SetActive(false);
            EventManager.Instance.PlayerDead();
        }
        
    }

    public void IncreasedHealth()
    {
        maxHealth = 150f;
        currentHealth = maxHealth;
    }



   
}
