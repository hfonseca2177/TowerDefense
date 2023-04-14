using System.Collections;
using TowerDefense.Enemies;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Tower that target and fires homing missiles
    /// </summary>
    public class AntiAirTower : BaseTower
    {
        [SerializeField] private GameObject _shootingEffect;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _shootingSpot;
        
        private float _elapsedTime; 
        
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
                    LookTarget();
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
        
        private void Fire()
        {
            _onCooldown = true;
            PlayFireSfx();
            _shootingEffect.SetActive(true);
            var missile = Instantiate(_projectile, _shootingSpot.position, _shootingSpot.rotation, transform);
            missile.Fire(_damage.CurrentValue, _speed.CurrentValue, _target);
            if(!_target.IsAlive) _targetLocked = false;
            StartCoroutine(StopFire());
        }

        private IEnumerator StopFire()
        {  
            yield return _fireDelay;
            _shootingEffect.SetActive(false);
        }
    }
}