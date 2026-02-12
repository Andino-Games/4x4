using System;
using UnityEngine;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float baseMaxHealth = 5f;
    public float maxHealth;
    public float currentHealth;
    private float difficultyScaling = 1f;
    public GameObject dropItemPrefab;


    [Header("Colores de Infección")]
    public Color healthyColor = Color.white; 
    public Color infectedColor = new Color(0.4f, 0.8f, 0.2f);
    public GameObject dieEffect;

    private SpriteRenderer sprite;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = healthyColor;
        transform.localScale = originalScale;

        float difficulty = 1+ (EnemyDifficulty.Instance.GetDifficulty() -1f) * difficultyScaling;
        maxHealth = baseMaxHealth * Mathf.Pow(difficulty,1f);
        currentHealth = maxHealth;
        Debug.Log("Enemy Spawned with Health: " + currentHealth);
    }

    public void TakeDamage(int damage, Vector3 sourcePos)
    {
        // 1. Apply Damage first
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
///////////////////////DOTween/////////////////////////
        // 2. Calculate Infection Color based on NEW health
        float infectionPercent = 1 - (currentHealth / maxHealth);
        Color currentColor = Color.Lerp(healthyColor, infectedColor, infectionPercent);
        sprite.DOColor(currentColor, 0.2f);

        // 3. Safe Punch animation
        transform.DOKill(); // Stop previous tweens
        transform.localScale = originalScale; // Reset to base
        transform.DOPunchScale(new Vector3(0.15f, -0.1f, 0), 0.2f, 5, 0.5f).OnComplete(() => transform.localScale = originalScale);

        // 4. Knockback (PunchPosition needs direction)
        Vector3 dir = (transform.position - sourcePos).normalized;
        transform.DOPunchPosition(dir * 0.2f, 0.2f, 10, 1);
        //////////////////////////////////////////////////
       
        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        // 1. Visual Feedback
        sprite.DOColor(Color.black, 0.3f);
        
        // 2. Death Animation -> Return to Pool
        transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).OnComplete(() => {
             EventManager.Instance.EnemyDie(gameObject); // Return to pool (which deactivates it)
        }); 
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        SpawnDrop();
    }

    private void SpawnDrop()
    {
       Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
    }
}
