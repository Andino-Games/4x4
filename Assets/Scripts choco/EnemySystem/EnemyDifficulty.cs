using System;
using UnityEngine;

public class EnemyDifficulty : MonoBehaviour
{
    public static EnemyDifficulty Instance;

    [Header("Difficulty Settings")]
    [SerializeField]private float difficulty = 1f;
    public float growthRate = 0.01f;
    public float maxDifficulty = 5f;   

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        float gameTime = Time.timeSinceLevelLoad;
        difficulty = 1f +  Mathf.Pow(gameTime * growthRate, 1.2f);
    }

    public float GetDifficulty()
    {
        return difficulty;
    }

}
