using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject dropItemPrefab;

    private void Start()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnEnemyDamaged += TakeDamage;
        }
        else
        {
            Debug.LogWarning("EventManager.Instance es null en EnemyHealth OnEnable. No se puede suscribir al evento.");
        }
        currentHealth = maxHealth;
    }
    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnEnemyDamaged -= TakeDamage;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Player Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        EventManager.Instance.EnemyDie(gameObject);
        Debug.Log("Enemy has died.");
        SpawnDrop();
    }

    private void SpawnDrop()
    {
       Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
    }
}
