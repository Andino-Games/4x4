using System;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
        public float speed = 10f;
        public float lifetime = 3f;
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
                        //Daño aca
                        Deactivate();
                }
        }
}