using System.Collections;
using TowerDefense.Enemies;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Quick shot tower - Fire quick shots to targets in range
    /// </summary>
    public class QuickShotTower : BaseTower
    {
        [SerializeField] private GameObject _shootingEffect;
        [SerializeField] private float _turnSpeed;
        
        private bool _targetLocked;
        private Enemy _target;
        private Rigidbody _rigidbody;
        private float _elapsedTime; 
        private bool _onCooldown;
        private int _multiBullet = 1;
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

        protected override void LoadAttributes()
        {
            base.LoadAttributes();
            AdjustMultiBullet();
        }

        public override void OnUpgradeEvent()
        {
            base.OnUpgradeEvent();
            AdjustMultiBullet();
        }

        private void AdjustMultiBullet()
        {
            if (!_special.IsUnlocked) return;
            var piercing = (int) _special.CurrentValue;
            _multiBullet = Mathf.Clamp(piercing, 1, piercing);
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
            for (int i = 0; i < _multiBullet; i++)
            {
                _target.TakeDamage(_damage.CurrentValue);
                if (!_target.IsAlive) break;
            }
            if(!_target.IsAlive) _targetLocked = false;
            StartCoroutine(StopFire());
        }

        private IEnumerator StopFire()
        {
            yield return new WaitForFixedUpdate();
            _shootingEffect.SetActive(false);
        }
    }
}
