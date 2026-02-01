using UnityEngine;

public class HealthExpansion : MonoBehaviour
{
    public void IncreaseMaxHealth()
    {
        EventManager.Instance.MaxHealthIncreased();
        
    }
}
