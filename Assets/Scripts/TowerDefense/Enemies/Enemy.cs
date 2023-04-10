using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Enemies
{
    public class Enemy : MonoBehaviour
    {
        
        private NavMeshAgent _agent;
        private Transform _target;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Move()
        {   
            _agent.destination = _target.position;
        }
    }
}
