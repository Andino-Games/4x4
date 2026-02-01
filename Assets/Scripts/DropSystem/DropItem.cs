using UnityEngine;

public class DropItem : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Stats stats = other.gameObject.GetComponent<Stats>();
            if(stats != null)
            {
                stats.AddXP(value);
            }
            Destroy(this.gameObject);
        }
    }
}
