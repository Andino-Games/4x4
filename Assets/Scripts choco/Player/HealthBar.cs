using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Las referencias pueden ser asignadas desde el Inspector.
    public Slider healthBar;
    public Health playerHealth;

    // Control para no spamear warnings cada frame si falta una referencia.
    private bool _warnedMissingReference;

    private void Awake()
    {
        // Intentar asignar automáticamente si no se han asignado desde el inspector.
        if (healthBar == null)
        {
            healthBar = GetComponent<Slider>() ?? GetComponentInChildren<Slider>();
        }

        if (playerHealth == null)
        {
            playerHealth = GetComponent<Health>() ?? GetComponentInParent<Health>() ?? FindFirstObjectByType<Health>();
        }
    }

    private void Update()
    {
        // Comprobar null para evitar NullReferenceException.
        if (healthBar == null || playerHealth == null)
        {
            if (!_warnedMissingReference)
            {
                Debug.LogWarning($"HealthBar: referencia(s) faltante(s). healthBar={(healthBar == null)}, playerHealth={(playerHealth == null)}", this);
                _warnedMissingReference = true;
            }
            return;
        }

        // Proteger contra división por cero y asegurar punto flotante.
        float maxHealth = playerHealth.maxHealth;
        float currentHealth = playerHealth.currentHealth;

        float normalized = 0f;
        if (maxHealth > 0f)
        {
            normalized = currentHealth / maxHealth;
        }

        healthBar.value = Mathf.Clamp01(normalized);
    }
}
