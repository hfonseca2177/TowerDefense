using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Singleton to return the current wave
    /// </summary>
    public class WaveListener : MonoBehaviour
    {
        [Tooltip("whenever a new Wave starts")]
        [SerializeField] private IntEventAsset _onNewWave;
        
        private static WaveListener _instance;
        public static WaveListener Instance => _instance;
        private int _currentWave;
        
        public int WaveIndex { get {return _currentWave;}} 
        
        private void Awake()
        {
            //singleton instance check
            if (_instance == null) {
                _instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            _onNewWave.OnInvoked.AddListener(OnNewWaveEvent);
        }

        private void OnDisable()
        {
            _onNewWave.OnInvoked.RemoveListener(OnNewWaveEvent);
        }

        private void OnNewWaveEvent(int wave)
        {
            _currentWave = wave;
        }
    }
}