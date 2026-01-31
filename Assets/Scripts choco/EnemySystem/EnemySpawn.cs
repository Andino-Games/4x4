using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public EnemyPool enemyPool;
    public Transform player;
    public float spawnRadius = 20f;
    public float spawnInterval = 5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);  
    }

    public void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetEnemy();
        Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
        enemy.transform.position = spawnPos;
    }


}
