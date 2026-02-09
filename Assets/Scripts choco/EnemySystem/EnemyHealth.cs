using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float baseMaxHealth;
    public float currentHealth;
    public GameObject dropItemPrefab;

    private void OnEnable()
    {
        float multiplier = EnemyDifficulty.Instance.GetHealthMultiplier();
        currentHealth = baseMaxHealth * multiplier;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);
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
