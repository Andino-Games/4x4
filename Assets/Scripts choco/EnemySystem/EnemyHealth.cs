using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject dropItemPrefab;

    private void OnEnable()
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
