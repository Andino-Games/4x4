using UnityEngine;

namespace Attack.AttackType
{
    public class BasicAttack : Attack
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
                float finalDamage = CalculateDamage();
                other.GetComponent<EnemyHealth>().TakeDamage(finalDamage, transform.position);
                Debug.Log("Enemy hit with basic attack for " + finalDamage + " damage.");
            }
        }
        public override void ExecuteAttack()
        {

        }
    }
}