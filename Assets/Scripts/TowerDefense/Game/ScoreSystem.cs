using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Score system - process all data from combat and converts to currency
    /// </summary>
    public class ScoreSystem : MonoBehaviour
    {
        [Header("Events")]
        [Tooltip("when a new game started")]
        [SerializeField] private VoidEventAsset _onGameStart;
        [Tooltip("when game is over")]
        [SerializeField] private VoidEventAsset _onGameOver;
        [Tooltip("whenever a new stage starts")]
        [SerializeField] private IntEventAsset _onNewStage;
        [Tooltip("whenever a new Wave starts")]
        [SerializeField] private IntEventAsset _onNewWave;
        [Tooltip("Notifies player damage taken")]
        [SerializeField] private IntEventAsset _onPlayerDamageTaken;
        [Tooltip("Notify enemy death")] 
        [SerializeField] private VoidEventAsset _onEnemyDeath;
        [Tooltip("Whenever enemy takes damage")] 
        [SerializeField] private FloatEventAsset _onEnemyDamageTaken;
        
        private GameScoreDAO _gameScoreDao;
        private GameScore _bestScore;
        private GameScore _currentScore;
        private float _startTime;
        private int _currentStage;
        private int _currentWave;

        private void Awake()
        {
            _gameScoreDao = GetComponent<GameScoreDAO>();
        }

        private void Start()
        {
            LoadData();
        }
        
        private void OnEnable()
        {
            _onGameStart.OnInvoked.AddListener(OnGameStartEvent);
            _onGameOver.OnInvoked.AddListener(OnGameOverEvent);
            _onNewStage.OnInvoked.AddListener(OnNewStageEvent);
            _onNewWave.OnInvoked.AddListener(OnNewWaveEvent);
            _onPlayerDamageTaken.OnInvoked.AddListener(OnDamageTakenEvent);
            _onEnemyDeath.OnInvoked.AddListener(OnEnemyDeathEvent);
            _onEnemyDamageTaken.OnInvoked.AddListener(OnEnemyDamageTakenEvent);
        }

        private void OnDisable()
        {
            _onGameStart.OnInvoked.RemoveListener(OnGameStartEvent);
            _onGameOver.OnInvoked.RemoveListener(OnGameOverEvent);
            _onNewStage.OnInvoked.RemoveListener(OnNewStageEvent);
            _onNewWave.OnInvoked.RemoveListener(OnNewWaveEvent);
            _onPlayerDamageTaken.OnInvoked.RemoveListener(OnDamageTakenEvent);
            _onEnemyDeath.OnInvoked.RemoveListener(OnEnemyDeathEvent);
            _onEnemyDamageTaken.OnInvoked.RemoveListener(OnEnemyDamageTakenEvent);
        }

        private void OnEnemyDeathEvent()
        {
            _currentScore.killCount++;
            //TODO _currentScore.score += GetScoreEnemyDeath(stage, wave)
        }


        private void OnEnemyDamageTakenEvent(float damage)
        {
            _currentScore.damageDone += damage;
            //TODO _currentScore.score += GetScoreEnemyDamageTake(stage, wave)
        }


        private void OnDamageTakenEvent(int damage)
        {
            _currentScore.damageTaken += damage;
            _currentScore.hitsTaken++;
        }

        private void OnNewWaveEvent(int wave)
        {
            _currentScore.waves++;
            _currentWave = wave;
        }

        private void OnNewStageEvent(int stage)
        {
            _currentScore.stages++;
            _currentStage = stage;
        }

        private void OnGameStartEvent()
        {
            _currentScore = new GameScore();
            _startTime = Time.time;
        }

        private void OnGameOverEvent()
        {
            SaveData();
        }

        private void LoadData()
        {
            _bestScore = _gameScoreDao.Retrieve();
            if (_bestScore == null) ResetData();
        }
        
        private void ResetData()
        {
            _bestScore = new GameScore();
            _gameScoreDao.Save(_bestScore);
        }
        
        private void SaveData()
        {
            _currentScore.timeElapsed = GetElapsedTime();
            _bestScore.UpdateRecords(_currentScore);
            _gameScoreDao.Save(_bestScore);
        }
        
        private float GetElapsedTime()
        {
            return Time.time - _startTime;
        }
    }
}