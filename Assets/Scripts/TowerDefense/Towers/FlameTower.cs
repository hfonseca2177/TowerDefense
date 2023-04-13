using System.Collections;
using TowerDefense.Enemies;
using UnityEngine;

namespace TowerDefense.Towers
{
    
    /// <summary>
    /// Flame Tower - does aoe damage within a range
    /// </summary>
    public class FlameTower : BaseTower
    {
        [Tooltip("Visual representation of the effect")]
        [SerializeField] private GameObject _areaEffect;
        private bool _isFiring;
        private bool _onCooldown;
        private float _elapsedTime;
        private float _snare = 0;

        private void FixedUpdate()
        {
            if (_isFiring) return;
            if (_onCooldown)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime < _speed.CurrentValue) return;
                _onCooldown = false;
            }
            else
            {
                DetectAndFireEnemy();    
            }
        }
        
        
        protected override void LoadAttributes()
        {
            base.LoadAttributes();
            AdjustSnareEffect();
        }

        public override void OnUpgradeEvent()
        {
            base.OnUpgradeEvent();
            AdjustSnareEffect();
        }

        private void AdjustSnareEffect()
        {
            if (!_special.IsUnlocked) return;
            _snare = _special.CurrentValue;
        }
        

        //if enemy is within range, no need to do projectile or other collision
        private void DetectAndFireEnemy()
        {
            _isFiring = true;
            Collider[] hits = new Collider[_detectionCap];
            var size = Physics.OverlapSphereNonAlloc(gameObject.transform.position, _range.CurrentValue, hits, _detectionLayer);
            if (size <= 0) return;
            Fire();
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] != null && hits[i].TryGetComponent(out Enemy enemy) && enemy.gameObject.activeSelf)
                {
                    enemy.TakeDamage(_damage.CurrentValue);
                    if (enemy.IsAlive && _snare > 0)
                    {
                        enemy.ApplySnareMovement(_snare);
                    }
                }
            }
            StartCoroutine(StopFire());
        }

        private void Fire()
        {
            _areaEffect.SetActive(true);
        }

        private IEnumerator StopFire()
        {
            yield return null;
            _areaEffect.SetActive(false);
            _elapsedTime = 0;
            _isFiring = false;
            _onCooldown = true;
        }
    }
}