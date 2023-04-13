using TMPro;
using TowerDefense.Events;
using TowerDefense.Game;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    /// <summary>
    /// Displays the current currency available
    /// </summary>
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private WalletSettings _walletSettings;
        [SerializeField] private TextMeshProUGUI _currencyName;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _currentCurrency;
        [Tooltip("Notifies when currency is updated")] 
        [SerializeField] private FloatEventAsset _onCurrencyUpdate;
        private void Awake()
        {
            _currencyName.text = _walletSettings.CurrencyName;
            _icon.sprite = _walletSettings.Icon;
        }

        private void OnEnable()
        {
            _onCurrencyUpdate.OnInvoked.AddListener(OnCurrencyUpdateEvent);
        }

        private void OnDisable()
        {
            _onCurrencyUpdate.OnInvoked.RemoveListener(OnCurrencyUpdateEvent);
        }

        private void OnCurrencyUpdateEvent(float currency)
        {
            var formattedCurrency = Mathf.FloorToInt(currency);
            _currentCurrency.text = formattedCurrency.ToString();
        }
    }
}