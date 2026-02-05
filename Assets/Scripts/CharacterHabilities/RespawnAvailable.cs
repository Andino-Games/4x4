using UnityEngine;

public class RespawnAvailable : MonoBehaviour
{
    public bool respawnAvailable = false;
    
    public void ActivateRespawn()
    {         EventManager.Instance.RespawnAvailable();
    }
    private void OnEnable()
    {
        ActivateRespawn();
    }
}
