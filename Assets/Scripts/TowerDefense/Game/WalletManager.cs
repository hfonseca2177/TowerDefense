using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Controls player currency
    /// </summary>
    public class WalletManager: MonoBehaviour
    {
        [Tooltip("Wallet configuration")]
        [SerializeField, Expandable] private WalletSettings _settings;
        [Tooltip("Serialized reference to this manager - alternative to singleton")] 
        [SerializeField] private WalletReference _walletReference; 
        [Tooltip("Notifies a new score awarded")] 
        [SerializeField] private FloatEventAsset _onNewScoreAwardedNotify;
        [Tooltip("Notifies a new successful purchase")] 
        [SerializeField] private FloatEventAsset _onPurchaseSpentNotify;
        [Tooltip("Notifies a new successful update")] 
        [SerializeField] private FloatEventAsset _onUpgradeSpentNotify;
        [Tooltip("Notifies when currency is updated")] 
        [SerializeField] private FloatEventAsset _onCurrencyUpdateNotify;

        private float _currentCurrency;

        private void Start()
        {
            _walletReference.Wallet = this;
            _currentCurrency = _settings.InitialBudget;
            _onCurrencyUpdateNotify.Invoke(_currentCurrency);
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
            _onCurrencyUpdateNotify.Invoke(_currentCurrency);
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
            cost = Mathf.FloorToInt(cost);
            bool hasFunds = _currentCurrency >= cost;
            if (hasFunds)
            {
                _currentCurrency -= cost;
                _onCurrencyUpdateNotify.Invoke(_currentCurrency);
            }
            return hasFunds;
        }
    }
}