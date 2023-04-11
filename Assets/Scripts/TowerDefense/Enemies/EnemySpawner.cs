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
        [SerializeField] private float _spawnInterval = 3f;
        [SerializeField] private GameObject _visualRepresentation;
        [SerializeField] private ObjectPooling _enemyPool;
        [SerializeField] private GameObjectEventAsset _onEnemyRelease;

        private float _elapsedTime;

        private void Start()
        {
            _visualRepresentation.SetActive(false);
        }

        private void OnEnable()
        {
            _onEnemyRelease.OnInvoked.AddListener(OnEnemyReleaseEvent);
        }

        private void OnDisable()
        {
            _onEnemyRelease.OnInvoked.RemoveListener(OnEnemyReleaseEvent);
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

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime < _spawnInterval) return;
            _elapsedTime = 0;
            SpawnEnemy();
        }
    }
}