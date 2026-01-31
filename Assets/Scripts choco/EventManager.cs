using System;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action<int> OnDamageTaken;
    public event Action<int> OnEnemyDamaged;

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

    public void EnemyDamageTaken(int damage)
    {
        OnEnemyDamaged?.Invoke(damage);
    }

}
