using TowerDefense.Enemies;
using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Game Manager -controls core components and game cycle
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform _playerBase;
        [SerializeField] private TargetReference _targetReference;
        [SerializeField] private float _delayToStartWaves;
        [SerializeField] private VoidEventAsset _onToggleWaves;

        private bool _gameOn;
        private float _elapsedTime;
         
        private void Awake()
        {
            _targetReference.Target = _playerBase;
        }

        private void Update()
        {
            if (_gameOn) return;
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime < _delayToStartWaves) return; 
            _gameOn = true;
            _onToggleWaves.Invoke();
        }
    }
}