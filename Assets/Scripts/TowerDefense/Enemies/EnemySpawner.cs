using System;
using TowerDefense.Events;
using TowerDefense.Util;
using UnityEngine;


namespace TowerDefense.Enemies
{
    /// <summary>
    /// Enemy spawner
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawningPoint;
        [SerializeField] private GameObject _visualRepresentation;
        [SerializeField] private ObjectPooling _enemyPool;
        [SerializeField] private GameObjectEventAsset _onEnemyRelease;
        [SerializeField] private VoidEventAsset _onSpawnRequest;

        private void Start()
        {
            _visualRepresentation.SetActive(false);
        }

        private void OnEnable()
        {
            _onEnemyRelease.OnInvoked.AddListener(OnEnemyReleaseEvent);
            _onSpawnRequest.OnInvoked.AddListener(OnSpawnRequestEvent);
        }

        private void OnDisable()
        {
            _onEnemyRelease.OnInvoked.RemoveListener(OnEnemyReleaseEvent);
            _onSpawnRequest.OnInvoked.RemoveListener(OnSpawnRequestEvent);
        }

        private void OnSpawnRequestEvent()
        {
            SpawnEnemy();
        }

        private void OnEnemyReleaseEvent(GameObject enemy)
        {
            _enemyPool.Release(enemy);
        }

        public void SpawnEnemy()
        {
            var enemy = _enemyPool.Get();
            var enemyTransform = enemy.transform;
            enemyTransform.position = _spawningPoint.position;
            enemyTransform.rotation = _spawningPoint.rotation;
            enemyTransform.parent = transform;
            enemy.SetActive(true);
            //GetUpdatedStats(enemy);
        }

        private void GetUpdatedStats(GameObject enemyObj)
        {
            if(enemyObj.TryGetComponent(out Enemy enemy))
            {
                //todo update the spawned enemy with current enemy type stats
                //it also could have a static reference in the enemy to avoid searching component
                //enemy.SetCurrentStats();
            }
            
        }

    }
}