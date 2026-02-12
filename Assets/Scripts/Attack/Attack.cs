using UnityEngine;
using UnityEngine.Rendering;

namespace Attack
{
    public abstract class Attack : MonoBehaviour
    {
        [Header("Stats Base")] 
        public int damage;
        public float fireRate;

        public float damageMultiplier = 0.85f;

        protected float NextAttackTime;
        
        public virtual void Update()
        {
            if (Time.time >= NextAttackTime)
            {
                ExecuteAttack();
                NextAttackTime = Time.time + fireRate;
            } 
        }

        protected float CalculateDamage()
        {
            float difficulty = EnemyDifficulty.Instance.GetDifficulty();
            return damage * Mathf.Pow(difficulty,damageMultiplier);
        }
        public abstract void ExecuteAttack();
    }
}
