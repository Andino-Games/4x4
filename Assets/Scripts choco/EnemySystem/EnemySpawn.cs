using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public EnemyPool enemyPool;
    public Transform player;
    public float spawnRadius = 20f;
    public float baseSpawnInterval = 1f;
    public float minSpawnInterval = 0.2f;
    public float difficultyGrowthRate = 0.05f;
    public float difficulty = 1;
    
    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, baseSpawnInterval);  
    }
    private void Update()
    {
        float currentInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval/ difficulty);
        difficulty += difficultyGrowthRate * Time.deltaTime;
    }

    public void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetEnemy();
        Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
        enemy.transform.position = spawnPos;
    }


}
