using TowerDefense.Events;
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
        [Tooltip("Enemy Base Stats")]
        [SerializeField] private EnemyDefinition _enemyDefinition;
        [Header("Events")]
        [Tooltip("Returns enemy back to pool")]
        [SerializeField] private GameObjectEventAsset _onReleaseEnemyNotify;
        [Tooltip("Notify enemy death")] 
        [SerializeField] private VoidEventAsset _onEnemyDeathNotify;
        [Tooltip("Whenever enemy takes damage")] 
        [SerializeField] private FloatEventAsset _onEnemyDamageTakenNotify;
        
        
        private NavMeshAgent _agent;
        private float _currentDamage;
        private float _currentHp;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _currentDamage = _enemyDefinition.BaseLineDamage;
        }

        private void OnEnable()
        {
            Move();
        }

        private void Move()
        {   
            _agent.destination = _targetReference.Target.position;
        }

        public float HitPlayer()
        {
            _onReleaseEnemyNotify.Invoke(this.gameObject);
            return _currentDamage;
        }

        public void TakeDamage(float damage)
        {
            _currentHp -= damage;
            _onEnemyDamageTakenNotify.Invoke(damage);
            if (_currentHp < 0)
            {
                _onEnemyDeathNotify.Invoke();
            }
        }

    }
}
