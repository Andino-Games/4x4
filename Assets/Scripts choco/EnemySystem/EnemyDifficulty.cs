using UnityEngine;

public class EnemyDifficulty : MonoBehaviour
{
    public static EnemyDifficulty Instance;

    [Header("Difficulty Settings")]
    public float difficulty = 1f;
    public float growthRate = 0.05f;

    [Header("Scaling Multipliers")]
    public float healthMultiplierPerDifficulty = 0.3f;
    public float spawnMultiplierPerDifficulty = 0.5f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        difficulty += growthRate * Time.deltaTime;
    }

    public float GetHealthMultiplier()
    {
        return 1f + difficulty * healthMultiplierPerDifficulty;
    }

    public float GetSpawnMultiplier()
    {
        return 1f + difficulty * spawnMultiplierPerDifficulty;
    }
}
