using UnityEngine;

namespace Attack.AttackType
{
    public class DoubleRotatingTipAttack : Attack
    {
        [Header("Spin Settings")]
        public float rotationdSpeed = 200f;

        public override void Update()
        {
            transform.Rotate(Vector3.forward * rotationdSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                float finalDamage = CalculateDamage() * 0.15f; // Double damage for this attack
                other.GetComponent<EnemyHealth>().TakeDamage(damage, transform.position);
                Debug.Log("Enemy hit with double rotating attack for " + damage + " damage.");
            }
        }

        public override void ExecuteAttack()
        {
            
        }

    }
}