using TMPro;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Load game data statistics to show summary
    /// </summary>
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTxt;
        [SerializeField] private TextMeshProUGUI _killsTxt;
        [SerializeField] private TextMeshProUGUI _timerTxt;
        [SerializeField] private TextMeshProUGUI _wavesTxt;
        [SerializeField] private TextMeshProUGUI _stageTxt;
        [SerializeField] private TextMeshProUGUI _dmgDoneTxt;
        [SerializeField] private TextMeshProUGUI _hitsTakenTxt;
        [SerializeField] private TextMeshProUGUI _dmgTakenTxt;
        
        private GameScoreDAO _gameScoreDao;
        private GameScore _bestScore;
        
        private void Awake()
        {
            _gameScoreDao = GetComponent<GameScoreDAO>();
        }
        
        private void Start()
        {
            LoadData();
        }
        private void LoadData()
        {
            _bestScore = _gameScoreDao.Retrieve();
            if (_bestScore == null) return;
            _scoreTxt.text = FloatToString(_bestScore.score);
            _killsTxt.text = _bestScore.killCount.ToString();
            _timerTxt.text = _bestScore.timeElapsed.ToString("F");
            _wavesTxt.text = FloatToString(_bestScore.waves);
            _stageTxt.text = FloatToString(_bestScore.stages);
            _dmgDoneTxt.text = FloatToString(_bestScore.damageDone);
            _hitsTakenTxt.text = FloatToString(_bestScore.hitsTaken);
            _dmgTakenTxt.text = FloatToString(_bestScore.damageTaken);
            
        }

        private string FloatToString(float value)
        {
            return Mathf.FloorToInt(value).ToString();
        }
        
    }
}