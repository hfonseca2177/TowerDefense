using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Controls player currency
    /// </summary>
    public class WalletManager: MonoBehaviour
    {
        [SerializeField] private WalletSettings _settings;
        [Tooltip("Notifies a new score awarded")] 
        [SerializeField] private FloatEventAsset _onNewScoreAwardedNotify;
        [Tooltip("Notifies a new successful purchase")] 
        [SerializeField] private FloatEventAsset _onPurchaseSpentNotify;
        [Tooltip("Notifies a new successful update")] 
        [SerializeField] private FloatEventAsset _onUpgradeSpentNotify;

        private float _currentCurrency;

        private void Start()
        {
            _currentCurrency = _settings.InitialBudget;
        }

        private void OnEnable()
        {
            _onNewScoreAwardedNotify.OnInvoked.AddListener(OnNewScoreEvent);
        }

        private void OnDisable()
        {
            _onNewScoreAwardedNotify.OnInvoked.RemoveListener(OnNewScoreEvent);
        }

        private void OnNewScoreEvent(float score)
        {
            _currentCurrency += _settings.ConversionRate * score;
        }

        public bool PurchaseTower(float cost)
        {
            bool success = TryPurchase(cost);
            if (success)
            {
                _onPurchaseSpentNotify.Invoke(cost);
            }
            return success;
        }
        
        public bool UpgradeTower(float cost)
        {
            bool success = TryPurchase(cost);
            if (success)
            {
                _onUpgradeSpentNotify.Invoke(cost);
            }
            return success;
        }

        private bool TryPurchase(float cost)
        {
            bool hasFunds = _currentCurrency > cost;
            if (hasFunds)
            {
                _currentCurrency -= cost;
            }
            return hasFunds;
        }
    }
}