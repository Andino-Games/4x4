using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
  [SerializeField]private SpriteRenderer sprite;
  [SerializeField]private Color damageColor;
  public float maxHealth = 100f;
  public float currentHealth;
  public Respawn respawn;  
  private Color originalColor;
  private Vector3 originalScale;

    private void Start()
    {
        originalColor = sprite.color;
        originalScale = transform.localScale;

        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnDamageTaken += TakeDamage;
            EventManager.Instance.OnMaxHealthIncreased += IncreasedHealth;
            EventManager.Instance.OnResetHealth += SetHealth;
        }
        else
        {
            Debug.LogWarning("EventManager.Instance es null en Health OnEnable. No se puede suscribir al evento.");
        }
        SetHealth();   
        respawn = GetComponent<Respawn>();
    }
    private void OnDisable()
    {
        EventManager.Instance.OnDamageTaken -= TakeDamage;
        EventManager.Instance.OnMaxHealthIncreased -= IncreasedHealth;
    }

    public void SetHealth()
    {
      currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Flash effect using Shader property
        if (sprite.material.HasProperty("_FlashAmount"))
        {
            sprite.material.SetFloat("_FlashAmount", 1f); // Start Full Flash
            sprite.material.DOFloat(0f, "_FlashAmount", 0.2f); // Fade out
        }
        else
        {
             // Fallback if shader is not assigned
             sprite.DOColor(damageColor, 0.2f).OnComplete(() => sprite.DOColor(originalColor, 0.2f));
        }
        transform.DOKill();
        
        Movement movement = GetComponent<Movement>();
        if (movement != null) movement.StopBreathing();
        else transform.localScale = originalScale;

        transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0), 0.2f, 10, 1).OnComplete(() => {
            transform.localScale = originalScale; // Reset to exact original
            if (movement != null) movement.StartBreathing();
        });

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
