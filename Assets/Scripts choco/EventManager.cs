using System;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action<int> OnDamageTaken;
    public event Action<GameObject> OnEnemyDied;

    [Header("Ability Events")]
    private int innecesaryValue;
    public event Action OnMaxHealthIncreased;
    public event Action OnRespawnAvailable;
    public event Action OnBetterSpeedActivated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void DamageTaken(int damage)
    {
        OnDamageTaken?.Invoke(damage);
    }

    public void EnemyDie(GameObject enemy)
    {
        OnEnemyDied?.Invoke(enemy);
    }

    #region Ability Events
    public void MaxHealthIncreased()
    {
        OnMaxHealthIncreased?.Invoke();
    }
    public void RespawnAvailable()
    {
        OnRespawnAvailable?.Invoke();
    }
    public void BetterSpeedActivated()
    {
        OnBetterSpeedActivated?.Invoke();
    }

    #endregion
}
