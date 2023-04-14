using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Score system - process all data from combat and converts to currency
    /// </summary>
    public class ScoreSystem : MonoBehaviour
    {
        [Header("Settings")] 
        [Tooltip("Score when player kills enemy")]
        [SerializeField, Expandable] private ScoreSettings _enemyKillScore; 
        [Tooltip("Score when player finishes a wave")]
        [SerializeField, Expandable] private ScoreSettings _finishWaveScore;
        [Tooltip("Score when player reaches anew stage")]
        [SerializeField, Expandable] private ScoreSettings _newStageScore;
        [Tooltip("Score based on time elapsed for the run")]
        [SerializeField, Expandable] private ScoreSettings _timeScore;
        
        
        [Header("Events")]
        [Tooltip("Notifies on general score update")] 
        [SerializeField] private FloatEventAsset _onScoreUpdateNotify;
        [Tooltip("Notifies a new score awarded")] 
        [SerializeField] private FloatEventAsset _onNewScoreAwardedNotify;
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
        [SerializeField] private FloatEventAsset _onEnemyDeath;
        [Tooltip("Whenever enemy takes damage")] 
        [SerializeField] private FloatEventAsset _onEnemyDamageTaken;
        
        private GameScoreDAO _gameScoreDao;
        private GameScore _bestScore;
        private GameScore _currentScore;
        private float _startTime;
        private float _waveStartTime;
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

        private void OnEnemyDeathEvent(float enemyScoreValue)
        {
            _currentScore.killCount++;
            var scoreAwarded = ScoreHelper.Instance.GetScoreEnemyDeath(enemyScoreValue, _currentWave,
                _enemyKillScore.FlatModifier, _enemyKillScore.PercentageModifier);
            _currentScore.score += scoreAwarded;
            _onScoreUpdateNotify.Invoke(_currentScore.score);
            _onNewScoreAwardedNotify.Invoke(scoreAwarded);
        }


        private void OnEnemyDamageTakenEvent(float damage)
        {
            _currentScore.damageDone += damage;
        }


        private void OnDamageTakenEvent(int damage)
        {
            _currentScore.damageTaken += damage;
            _currentScore.hitsTaken++;
        }

        private void OnNewWaveEvent(int wave)
        {
            float waveElapsedTime = Time.time - _waveStartTime;  
            _currentScore.waves++;
            _waveStartTime = Time.time;
            //if first wave, none completed yet
            if(_currentScore.waves == 1) return; 
            //scores by finishing current wave
            ScoreNewWave();
            //scores by timer
            ScoreTimer(waveElapsedTime);
            //once award score from previous wave, update current reference
            _currentWave = wave;
        }

        private void ScoreNewWave()
        {
            var scoreAwarded = ScoreHelper.Instance.GetNewWaveCompleted(_finishWaveScore.BaseValue, _currentWave,
                _finishWaveScore.FlatModifier, _finishWaveScore.PercentageModifier);
            _currentScore.score += scoreAwarded;
            _onScoreUpdateNotify.Invoke(_currentScore.score);
            _onNewScoreAwardedNotify.Invoke(scoreAwarded);
        }

        private void ScoreTimer(float elapsedTime)
        {
            var scoreAwarded = ScoreHelper.Instance.GetScoreByTimeElapsed(elapsedTime, _currentWave,
                _timeScore.FlatModifier, _timeScore.PercentageModifier, _timeScore.Rate);
            _currentScore.score += scoreAwarded;
            _onScoreUpdateNotify.Invoke(_currentScore.score);
            _onNewScoreAwardedNotify.Invoke(scoreAwarded);
        }
        
        private void OnNewStageEvent(int stage)
        {
            _currentScore.stages++;
            _currentStage = stage;
            var scoreAwarded = ScoreHelper.Instance.GetNewWaveCompleted(_newStageScore.BaseValue, _currentStage,
                _newStageScore.FlatModifier, _newStageScore.PercentageModifier);
            _currentScore.score += scoreAwarded;
            _onScoreUpdateNotify.Invoke(_currentScore.score);
            _onNewScoreAwardedNotify.Invoke(scoreAwarded);
        }

        private void OnGameStartEvent()
        {
            _currentScore = new GameScore();
            _startTime = Time.time;
            _waveStartTime = Time.time;
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