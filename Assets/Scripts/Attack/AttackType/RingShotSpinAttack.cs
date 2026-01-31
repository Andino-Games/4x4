using UnityEngine;
using UnityEngine.Pool;

namespace Attack.AttackType
{
    public class RingShotSpinAttack : Attack
    {
        [Header("Radial Settings")]
        public GameObject bulletPrefab;
        public int bulletCount = 10;
        public float radius = 0.5f;

        [Header("Pool Settings")] 
        public int poolDefaultSize = 20;
        public int poolMaxSize = 50;
        private IObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(
                CreateBullet, OnGetBullet, OnRealeaseBullet, OnDestroyBullet,
                true, poolDefaultSize, poolMaxSize);
        }
        //espiral
        public override void Update()
        {
            base.Update();
            transform.Rotate(0,0,100f * Time.deltaTime);
        }

        Bullet CreateBullet()
        {
            GameObject go = Instantiate(bulletPrefab);
            Bullet b = go.GetComponent<Bullet>();
            b.SetPool(_bulletPool);
            return b;
        }
        void OnGetBullet(Bullet bullet) => bullet.gameObject.SetActive(true);
        void OnRealeaseBullet(Bullet bullet) => bullet.gameObject.SetActive(false);
        void OnDestroyBullet(Bullet bullet) => Destroy(bullet.gameObject);
        
        public override void ExecuteAttack()
        {
            SpawnBullet();
        }

        void SpawnBullet()
        {
            float angleStep = 360f / bulletCount;
            float angle = transform.eulerAngles.z;
            // float angle = 0f;

            for (int i = 0; i < bulletCount; i++)
            {
                float x = transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float y = transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                
                Bullet b = _bulletPool.Get();
                b.transform.position = new Vector3(x, y, 0);
                b.transform.rotation = Quaternion.Euler(0, 0, angle);

                angle += angleStep;
            }
            
        }
    }
}