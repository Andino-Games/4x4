using UnityEngine;

namespace Attack.AttackType
{
    public class AreaIncreaseAttack : Attack
    {
        [Header("Spin Settings")]
        public float rotationdSpeed = 200f;
        public override void ExecuteAttack()
        {
            
        }

        public override void Update()
        {
            transform.Rotate(Vector3.forward * rotationdSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {

                other.GetComponent<EnemyHealth>().TakeDamage(damage);
                Debug.Log("Enemy hit with area increased for " + damage + " damage.");
            }
        }
    }
}
