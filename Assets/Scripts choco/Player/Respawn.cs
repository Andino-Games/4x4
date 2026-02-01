using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;
    public bool canRespawn = false;

    private void Start()
    {
        if (respawnPoint == null)
        {
            Debug.LogError("Respawn point not set on " + gameObject.name);
        }
        EventManager.Instance.OnRespawnAvailable += RespawnAvailable;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnRespawnAvailable -= RespawnAvailable;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (canRespawn && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            canRespawn = false;
        }
        else
        {
            Debug.LogWarning("Respawn not available or respawn point not set for " + player.name);
        }
    }

    public void RespawnAvailable()
    {
      canRespawn = true;
    }
}
