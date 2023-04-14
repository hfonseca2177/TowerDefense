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
        [SerializeField] private EnemyDefinition _enemyDefinition;
        [SerializeField] private EnemyLevelReference _enemyLevelReference;
        [SerializeField] private Transform _spawningPoint;
        [SerializeField] private GameObject _visualRepresentation;
        [SerializeField] private ObjectPooling _enemyPool;
        [SerializeField] private GameObjectEventAsset _onEnemyRelease;
        [SerializeField] private VoidEventAsset _onSpawnRequest;
        [SerializeField] private IntEventAsset _onNewWave;

        private EnemyStatsDTO _damage;
        private EnemyStatsDTO _speed;
        private EnemyStatsDTO _hitPoints;

        private void Start()
        {
            _visualRepresentation.SetActive(false);
            LoadAttributes();
        }

        private void LoadAttributes()
        {
            _damage = new EnemyStatsDTO(_enemyDefinition.Damage);
            _speed = new EnemyStatsDTO(_enemyDefinition.Speed);
            _hitPoints = new EnemyStatsDTO(_enemyDefinition.HitPoints);
            _enemyLevelReference.Damage = _damage;
            _enemyLevelReference.Speed = _speed;
            _enemyLevelReference.HitPoints = _hitPoints;
        }

        private void OnEnable()
        {
            _onNewWave.OnInvoked.AddListener(OnNewWaveEvent);
            _onEnemyRelease.OnInvoked.AddListener(OnEnemyReleaseEvent);
            _onSpawnRequest.OnInvoked.AddListener(OnSpawnRequestEvent);
        }

        private void OnDisable()
        {
            _onNewWave.OnInvoked.RemoveListener(OnNewWaveEvent);
            _onEnemyRelease.OnInvoked.RemoveListener(OnEnemyReleaseEvent);
            _onSpawnRequest.OnInvoked.RemoveListener(OnSpawnRequestEvent);
        }

        private void OnNewWaveEvent(int wave)
        {
            _damage.ScaleUp(wave);
            _speed.ScaleUp(wave);
            _hitPoints.ScaleUp(wave);
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
        }

    }
}