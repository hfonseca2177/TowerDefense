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
        [Tooltip("Delay before start first wave")]
        [SerializeField] private float _delayToStartWaves;
        [Header("Events")]
        [Tooltip("Notify wave system to start waves")]
        [SerializeField] private VoidEventAsset _onToggleWavesNotify;
        [Tooltip("Notify a new game started")]
        [SerializeField] private VoidEventAsset _onGameStartNotify;
        [Tooltip("Notify when game is over")]
        [SerializeField] private VoidEventAsset _onGameOverNotify;
        [Tooltip("Notify player has no more HP")]
        [SerializeField] private VoidEventAsset _onPlayerKilled;
        

        private bool _gameOn;
        private float _elapsedTime;
         
        private void Awake()
        {
            _targetReference.Target = _playerBase;
        }

        private void Start()
        {
            _onGameStartNotify.Invoke();
        }

        private void OnEnable()
        {
            _onPlayerKilled.OnInvoked.AddListener(OnPlayerKilledEvent);
        }

        private void OnDisable()
        {
            _onPlayerKilled.OnInvoked.RemoveListener(OnPlayerKilledEvent);
        }

        private void OnPlayerKilledEvent()
        {
            _onGameOverNotify.Invoke();
        }

        private void Update()
        {
            if (_gameOn) return;
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime < _delayToStartWaves) return; 
            _gameOn = true;
            _onToggleWavesNotify.Invoke();
        }
    }
}