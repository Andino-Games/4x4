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

    private float spawnTimer = 0f;

    private void Update()
    {
        float difficulty = EnemyDifficulty.Instance.GetSpawnMultiplier();
        float currentInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval / difficulty );
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
