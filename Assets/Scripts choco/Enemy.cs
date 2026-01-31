using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10;

    /* private void Start()
     {
         Debug.Log($"Enemy active={gameObject.activeInHierarchy}, enabled={enabled}");
         var rb = GetComponent<Rigidbody2D>();
         var col = GetComponent<Collider2D>();
         Debug.Log($"Rigidbody2D present={(rb!=null)}, Collider2D present={(col!=null)}, Collider isTrigger={(col!=null ? col.isTrigger.ToString() : "N/A")}");
     }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D called with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.DamageTaken(damage);
            Debug.Log("Enemy dealt " + damage + " damage to Player.");
        }
    }

}

