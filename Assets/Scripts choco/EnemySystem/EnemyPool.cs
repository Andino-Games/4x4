using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
   public List<GameObject> enemyPrefab;
    public int poolSize = 10;

   private Queue<GameObject> enemyPool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Count);
            GameObject enemy = Instantiate(enemyPrefab[randomIndex]);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }
    private void Start()
    {
        EventManager.Instance.OnEnemyDied += ReturEnemy;
    }

    public GameObject GetEnemy()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }

        GameObject newEnemy = Instantiate(enemyPrefab[0]);
        return newEnemy;
    }


    public void ReturEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }

}
