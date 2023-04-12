using TMPro;
using TowerDefense.Events;
using TowerDefense.Player;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    /// <summary>
    /// UI to display player current Hit points
    /// </summary>
    public class PlayerHpDisplay : MonoBehaviour
    {
        
        [Tooltip("Player baseline stats like Hit Points")]
        [SerializeField] private PlayerBaseStats _playerBaseStats;
        [Tooltip("Notifies current player Hit Points")]
        [SerializeField] private IntEventAsset _onPlayerHpUpdate;
        [SerializeField] private TextMeshProUGUI _percentage;
        [SerializeField] private Image _fillBar;

        private int _maxHp;
        private int _currentHp;

        private void Awake()
        {
            _maxHp = _playerBaseStats.HitPoints;
            _currentHp = _playerBaseStats.HitPoints;
        }

        private void OnEnable()
        {
            _onPlayerHpUpdate.OnInvoked.AddListener(OnPlayerHPUpdateEvent);
        }

        private void OnDisable()
        {
            _onPlayerHpUpdate.OnInvoked.RemoveListener(OnPlayerHPUpdateEvent);
        }

        private void OnPlayerHPUpdateEvent(int currentHp)
        {
            _currentHp = currentHp;
            var percentage =_currentHp / (float)_maxHp;
            _fillBar.fillAmount = percentage;
            _percentage.text = percentage.ToString();
        }
    }
}