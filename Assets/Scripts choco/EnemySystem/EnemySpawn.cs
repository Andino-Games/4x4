using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("References")]
    public EnemyPool enemyPool;
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnRadius = 20f;
    public float baseSpawnInterval = 1f;
    public float minSpawnInterval = 0.2f;
    private float difficultyScaling = 0.8f;
    private float spawnTimer = 0f;

    private void Update()
    {
        float difficulty = 1f+ (EnemyDifficulty.Instance.GetDifficulty() -1f) * difficultyScaling;
        float currentInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval / Mathf.Pow(difficulty,1.2f));
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    public void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetEnemy();
        if (enemy == null) return;
        Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
        enemy.transform.position = spawnPos;
    }
}
