using System;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action<float> OnDamageTaken;
    public event Action<GameObject> OnEnemyDied;
    public event Action OnPlayerDied;

    [Header("Ability Events")]
    private int innecesaryValue;
    public event Action OnMaxHealthIncreased;
    public event Action OnRespawnAvailable;
    public event Action OnBetterSpeedActivated;
    public event Action OnResetHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
         }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void DamageTaken(float damage)
    {
        OnDamageTaken?.Invoke(damage);
    }

    public void PlayerDead()
    {
        OnPlayerDied?.Invoke();
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

    public void ResetHealth()
    {
        OnResetHealth?.Invoke();
    }

    #endregion
}
