using UnityEngine;
using DG.Tweening; // ¡No olvides DOTween!

public class BackgroundPulse : MonoBehaviour
{
    [Header("Configuración de Capas")]
    public SpriteRenderer[] backgroundLayers; // Arrastra aquí todas tus capas de fondo

    [Header("Ajustes del Pulso")]
    public Color pulseColor = new Color(1f, 0.6f, 0.6f); // Un rojo suave/rosado
    public float duration = 2f; // Faster for testing

    private Color[] originalColors;

    void Awake()
    {
        if (backgroundLayers == null || backgroundLayers.Length == 0)
        {
            Debug.LogWarning("BackgroundPulse: No layers assigned!");
            return;
        }

        originalColors = new Color[backgroundLayers.Length];
        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            if (backgroundLayers[i] != null)
                originalColors[i] = backgroundLayers[i].color;
        }
    }

    void OnEnable()
    {
        if (backgroundLayers == null) return;
        Debug.Log($"BackgroundPulse: Starting pulse on {backgroundLayers.Length} layers.");

        float delay = 0f;
        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            SpriteRenderer layer = backgroundLayers[i];
            if (layer != null)
            {
                // Reset to original color to avoid drift if re-enabled mid-tween
                if (originalColors != null && i < originalColors.Length)
                    layer.color = originalColors[i];

                layer.DOColor(pulseColor, duration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine)
                    .SetDelay(delay);

                delay += 0.2f;
            }
        }
    }

    private void OnDisable()
    {
        foreach (SpriteRenderer layer in backgroundLayers)
        {
            layer.DOKill();
        }
    }
}