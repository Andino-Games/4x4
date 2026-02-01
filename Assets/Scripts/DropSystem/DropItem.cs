using UnityEngine;
using UnityEngine.UI;


public class DropItem : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Stats stats = other.GetComponent<Stats>();

            if (stats != null)
            {
                stats.AddXP(value);
            }

            Destroy(gameObject);
        }
    }
}
