using TowerDefense.Events;
using TowerDefense.Util;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Base enemy behavior
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [Tooltip("Reference to player base as target")]
        [SerializeField] private TargetReference _targetReference;
        [Tooltip("Reference to current enemy stats scale")]
        [SerializeField] private EnemyLevelReference _levelReference;
        [Tooltip("Enemy Base Stats")]
        [SerializeField] private EnemyDefinition _enemyDefinition;
        [Header("Events")]
        [Tooltip("Returns enemy back to pool")]
        [SerializeField] private GameObjectEventAsset _onReleaseEnemyNotify;
        [Tooltip("Notify enemy death")] 
        [SerializeField] private FloatEventAsset _onEnemyDeathNotify;
        [Tooltip("Whenever enemy takes damage")] 
        [SerializeField] private FloatEventAsset _onEnemyDamageTakenNotify;

        private float _damage;
        public float _hitPoints;
        private float _speed;
      
        public bool IsAlive
        {
            get { return _hitPoints > 0; }
        }

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            LoadStats();
            Move();
        }

        private void LoadStats()
        {
            _damage = _levelReference.Damage.CurrentValue;
            _hitPoints = _levelReference.HitPoints.CurrentValue;
            _speed = _levelReference.Speed.CurrentValue;
            _agent.speed = _speed;
        }

        private void Move()
        {   
            _agent.destination = _targetReference.Target.position;
        }

        public float HitPlayer()
        {
            _onReleaseEnemyNotify.Invoke(this.gameObject);
            return _damage;
        }

        public void TakeDamage(float damage)
        {
            _hitPoints -= damage;
            _onEnemyDamageTakenNotify.Invoke(damage);
            if (_hitPoints > 0) return;
            _onEnemyDeathNotify.Invoke(_enemyDefinition.Score);
            _onReleaseEnemyNotify.Invoke(this.gameObject);
        }

        public void ApplySnareMovement(float amount)
        {
            _agent.speed -= amount;
        }
        

    }
}
