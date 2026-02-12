using System;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
        public float speed = 10f;
        public float lifetime = 1f;
        public int damage = 5;
        private float _timer;
        private IObjectPool<Bullet>  _myPool;
        
        public void SetPool(IObjectPool<Bullet> pool) => _myPool = pool;

        private void OnEnable()
        {
                _timer = lifetime;
        }

        private void Update()
        {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                _timer -= Time.deltaTime;
                if (_timer <= 0) Deactivate();
        }

        void Deactivate()
        {
                if(_myPool != null) _myPool.Release(this);
                else gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
                if (other.CompareTag("Enemy"))
                {
                    float finalDamage = CalculateDamage();
                    other.GetComponent<EnemyHealth>().TakeDamage(finalDamage, transform.position);
                    Debug.Log("Enemy hit with basic attack for " + finalDamage + " damage.");
                    Deactivate();
                }
        }

    #region calculateDamage
        public float CalculateDamage()
        {
                float difficulty = EnemyDifficulty.Instance.GetDifficulty();
                float factor = 1f + (difficulty - 1f) * 0.2f;
                factor = Mathf.Min(factor, 2f);
                return damage * factor;
    }
    #endregion
}