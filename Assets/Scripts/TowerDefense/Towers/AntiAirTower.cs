using System.Collections;
using TowerDefense.Enemies;
using UnityEngine;

namespace TowerDefense.Towers
{
    public class AntiAirTower : BaseTower
    {
        [SerializeField] private GameObject _shootingEffect;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _shootingSpot;
        
        private bool _targetLocked;
        private Enemy _target;
        private Rigidbody _rigidbody;
        private float _elapsedTime; 
        private bool _onCooldown;
        private void FixedUpdate()
        {
            if (_onCooldown)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime < _speed.CurrentValue) return;
                _onCooldown = false;
            }
            else
            {
                if (_targetLocked)
                {
                    var direction = _target.transform.position - transform.position;
                    LookTarget(direction);
                    Fire();    
                }
                else
                {
                    AcquireTarget();
                }
            }
        }
        
        private void AcquireTarget()
        {
            Collider[] hits = new Collider[_detectionCap];
            var size = Physics.OverlapSphereNonAlloc(gameObject.transform.position, _range.CurrentValue, hits, _detectionLayer);
            if (size <= 0) return;
            if (hits[0] != null && hits[0].TryGetComponent(out Enemy enemy) && enemy.gameObject.activeSelf)
            {
                _targetLocked = true;
                _target = enemy;
            }
        }

        private void LookTarget(Vector3 direction)
        {
            if (direction.magnitude > float.Epsilon) return;
            var lookDirection = new Vector3(direction.x, 0f, direction.z).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed  * Time.deltaTime);
            _rigidbody.MoveRotation(rotation);
        }
        
        private void Fire()
        {
            _onCooldown = true;
            _shootingEffect.SetActive(true);
            var missile = Instantiate(_projectile, _shootingSpot.position, _shootingSpot.rotation, transform);
            
            if (_target.IsAlive) return;
            _targetLocked = false;
            StartCoroutine(StopFire());
        }

        private IEnumerator StopFire()
        {
            yield return new WaitForFixedUpdate();
            _shootingEffect.SetActive(false);
        }
    }
}