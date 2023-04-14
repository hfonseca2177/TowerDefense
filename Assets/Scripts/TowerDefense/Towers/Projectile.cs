using System.Collections;
using TowerDefense.Enemies;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Basic Projectile 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile settings")] 
        [SerializeField] protected float _speed = 15f;
        [Header("Projectile settings")] 
        [SerializeField] protected float _rotateSpeed = 150f;
        [SerializeField] protected float _damage = 1f;
    
        [SerializeField, Tooltip("Time before the projectile is destroyed")]
        protected float _lifeTime = 3f;

        [SerializeField, Tooltip("If should remain until life timeout after any impact")]
        protected bool _destroyOnImpact;
        
        private Vector3 _standardPrediction, _deviatedPrediction;

        private Rigidbody _rigidBody;

        //accumulates the amount of time spawned
        private float _spawnTime;

        //used to control the realtime when it was actually launched and not instantiated
        private bool _launched;

        private Enemy _target;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!_launched) return;
            //accumulates the spawn time
            _spawnTime += Time.deltaTime;
            //if life time is gone destroy it
            if (_spawnTime > _lifeTime)
            {
                StartCoroutine(DestroyProjectile());
            }
        }

        //Starts projectile physics
        public void Fire(float damage, float speed, Enemy target)
        {
            _target = target;
            _damage = damage;
            //_speed = speed;
            
            _launched = true;
        }

        private void FixedUpdate()
        {
            if (!_launched) return;
            var projectileTransform = transform;
            var targetPosition = _target.transform.position;
            var position = projectileTransform.position;
            var missilePosition = position;
            projectileTransform.LookAt(targetPosition);
            var direction = (targetPosition - missilePosition);
            direction.Normalize();
            position += direction * _speed * Time.deltaTime;
            projectileTransform.position = position;
        }
     

        //case projectile collider are used as trigger
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Enemy enemy)) return;
           DealDamage(enemy);
        }

        //Case projectile uses normal Collider 
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.TryGetComponent(out Enemy enemy)) return;
            DealDamage(enemy);
        }

        //Applies damage to the victim health component
        private void DealDamage(Enemy enemy)
        {
            enemy.TakeDamage(_damage);
            if (_destroyOnImpact)
            {
                StartCoroutine(DestroyProjectile());
            }
        }

        IEnumerator DestroyProjectile()
        {
            yield return null;
            Destroy(gameObject);
        }
    }
}