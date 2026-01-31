using UnityEngine;

namespace Attack
{
    public abstract class Attack : MonoBehaviour
    {
        [Header("Stats Base")] 
        public int damage;
        public float fireRate;

        protected float NextAttackTime;
        
        public virtual void Update()
        {
            if (Time.time >= NextAttackTime)
            {
                ExecuteAttack();
                NextAttackTime = Time.time + fireRate;
            } 
        }
        public abstract void ExecuteAttack();
    }
}
