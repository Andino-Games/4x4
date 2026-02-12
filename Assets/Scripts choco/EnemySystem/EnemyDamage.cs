using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float baseDamage = 10f;
    public float damage = 10f;
    public float damageInterval = 3f;
    public float damageTimer = 0f;
    private float difficultyScaling = 0.2f;
    private bool playerInRange = false;

    private void Update()
    {
        if(playerInRange)
        {
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageInterval)
            {
                
                if (EventManager.Instance != null)
                {
                    float scaled = GetScaledDamage();
                    EventManager.Instance.DamageTaken(scaled);
                    Debug.Log("Player takes " + scaled + " damage from enemy.");
                    damageTimer = 0f;
                }
                else
                {
                    Debug.LogWarning("EventManager.Instance es null. No se puede aplicar daño.");
                }
            }
        }
    }

    public float GetScaledDamage()
    {
        float difficulty = EnemyDifficulty.Instance.GetDifficulty();
        float factor = 1f + (difficulty - 1f) * difficultyScaling;
        factor = Mathf.Min(factor, 2f);
        return damage * factor;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            damageTimer = damageInterval;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


}

