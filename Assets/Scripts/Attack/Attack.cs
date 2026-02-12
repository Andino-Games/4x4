using UnityEngine;
using UnityEngine.Rendering;

namespace Attack
{
    public abstract class Attack : MonoBehaviour
    {
        [Header("Stats Base")] 
        public int damage;
        public float fireRate;
        public float scaling;
       
        protected float NextAttackTime;
        
        public virtual void Update()
        {
            if (Time.time >= NextAttackTime)
            {
                ExecuteAttack();
                NextAttackTime = Time.time + CalculateFireRate();
            } 
        }

        protected float CalculateDamage()
        {
            float difficulty = EnemyDifficulty.Instance.GetDifficulty();
            float factor = 1f + (difficulty - 1f) * scaling;
            factor = Mathf.Min(factor,2f);
            return damage * factor;
        }

        protected float CalculateFireRate()
        {
            float difficulty = EnemyDifficulty.Instance.GetDifficulty();
            float scaledRate = fireRate / (1f + (difficulty - 1f) * scaling);

            return Mathf.Clamp(scaledRate, 0.15f, fireRate);
        }
        public abstract void ExecuteAttack();
    }
}
