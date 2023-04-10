using UnityEngine;

namespace TowerDefense.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _spawningPoint;
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private float _spawnInterval = 3f;
        
        private float _elapsedTime;

        public void SpawnEnemy()
        {
            var enemy = Instantiate(_enemyPrefab, _spawningPoint.position, _spawningPoint.rotation, _spawningPoint.transform);
            enemy.SetTarget(_targetPoint);
            enemy.Move();
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