using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float baseDamage = 10f;
    public float damage = 10f;
    public float damageInterval = 3f;
    public float damageTimer = 0f;
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
        damage = baseDamage * Mathf.Pow(difficulty, 0.8f);
        return damage;
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

