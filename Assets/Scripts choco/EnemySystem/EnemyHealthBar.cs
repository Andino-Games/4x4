using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public EnemyHealth enemyHealth;

    // Control para no spamear warnings cada frame si falta una referencia.
    private bool _warnedMissingReference;

    private void Awake()
    {
        if (healthBar == null)
        {
            healthBar = GetComponent<Slider>() ?? GetComponentInChildren<Slider>();
        }

        if (enemyHealth == null)
        {
            enemyHealth = GetComponent<EnemyHealth>() ?? GetComponentInParent<EnemyHealth>() ?? FindFirstObjectByType<EnemyHealth>();
        }
    }

    private void Update()
    {
        if (healthBar == null || enemyHealth == null)
        {
            if (!_warnedMissingReference)
            {
                Debug.LogWarning($"HealthBar: referencia(s) faltante(s). healthBar={(healthBar == null)}, playerHealth={(enemyHealth == null)}", this);
                _warnedMissingReference = true;
            }
            return;
        }
/*
        // Proteger contra división por cero y asegurar punto flotante.
        float maxHealth = enemyHealth.baseMaxHealth;
        float currentHealth = enemyHealth.currentHealth;

        float normalized = 0f;
        if (maxHealth > 0f)
        {
            normalized = currentHealth / maxHealth;
        }

        healthBar.value = Mathf.Clamp01(normalized);*/
    }
}
