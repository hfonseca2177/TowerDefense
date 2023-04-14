using System.Collections;
using TowerDefense.Enemies;
using TowerDefense.Events;
using TowerDefense.Util;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Tower that target and fires homing missiles
    /// </summary>
    public class AntiAirTower : BaseTower
    {
        [SerializeField] private GameObject _shootingEffect;
        [SerializeField] private Transform _shootingSpot;
        [SerializeField] private ObjectPoolingReference _projectilePoolingReference;
        [SerializeField] private GameObjectEventAsset _onProjectileRelease;

        private ObjectPooling _projectilePool; 
        
        private float _elapsedTime;

        protected override void Start()
        {
            base.Start();
            _projectilePool = _projectilePoolingReference.Pool;
        }

        private void OnEnable()
        {
            _onProjectileRelease.OnInvoked.AddListener(OnProjectileReleaseEvent);
        }

        private void OnDisable()
        {
            _onProjectileRelease.OnInvoked.RemoveListener(OnProjectileReleaseEvent);
        }

        private void OnProjectileReleaseEvent(GameObject projectile)
        {
            _projectilePool.Release(projectile);
        }

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
            var missileObj = _projectilePool.Get();
            if(missileObj.TryGetComponent(out Projectile missile))
            {
                var missileTransform = missileObj.transform;
                missileTransform.position = _shootingSpot.position;
                missileTransform.rotation = _shootingSpot.rotation;
                missile.Fire(_damage.CurrentValue, _target);
                missileObj.SetActive(true);
                if(!_target.IsAlive) _targetLocked = false;
                StartCoroutine(StopFire());
            }
        }

        private IEnumerator StopFire()
        {  
            yield return _fireDelay;
            _shootingEffect.SetActive(false);
        }
    }
}