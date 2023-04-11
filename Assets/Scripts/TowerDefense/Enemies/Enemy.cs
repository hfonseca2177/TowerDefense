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
        [Tooltip("Returns enemy back to pool")]
        [SerializeField] private GameObjectEventAsset _onReleaseEnemyNotify;
        [Tooltip("Enemy Base Stats")]
        [SerializeField] private EnemyDefinition _enemyDefinition;
        
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            Move();
        }

        private void Move()
        {   
            _agent.destination = _targetReference.Target.position;
        }

        public void HitPlayer()
        {
            _onReleaseEnemyNotify.Invoke(this.gameObject);
        }

        public void TakeDamage(float damage)
        {
            //TODO get current damage based on stage _enemyDefinition.BaseLineDamage;
            //create a state for base stats
        }
    }
}
