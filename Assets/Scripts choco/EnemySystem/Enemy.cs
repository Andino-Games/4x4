using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10;
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
                    EventManager.Instance.DamageTaken(damage);
                    Debug.Log($"Enemy dealt {damage} damage to Player (time={Time.time}).");
                    Debug.Log($"TakeDamage frame {Time.frameCount}");
                    damageTimer = 0f;
                }
                else
                {
                    Debug.LogWarning("EventManager.Instance es null. No se puede aplicar daño.");
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            damageTimer = 0;
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

