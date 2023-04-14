using TMPro;
using TowerDefense.Events;
using TowerDefense.Game;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Tower Upgrade Manager
    /// </summary>
    public class TowerUpgradePanel : MonoBehaviour
    {
        [Tooltip("Type of tower definition")]
        [SerializeField] private TowerDefinition _towerDefinition;
        [Tooltip("Serialized reference to this manager - alternative to singleton")] 
        [SerializeField] private WalletReference _walletReference;
        [SerializeField] private GameObject _upgradeButton;
        [Tooltip("Pops a message to user")]
        [SerializeField] private StringEventAsset _notifyUser;
        [SerializeField] private IntEventAsset _onNewWave;
        [SerializeField] private TextMeshProUGUI _costTxt;
        [SerializeField] private TextMeshProUGUI _levelTxt;
        
        [SerializeField] private UnityEvent _onUpgradeTowerTrigger;
        
        private WalletManager _walletManager;
        private int _currentLevel = 1;
        private float _currentTowerCost;

        private void Start()
        {
            _walletManager = _walletReference.Wallet;
            UpdateLevelInfo();
            UpdateCost(WaveListener.Instance.WaveIndex);
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
            UpdateCost(wave);
        }
        
        private void UpdateCost(int wave)
        {
            _currentTowerCost = TowerCostHelper.Instance.GetUpgradeCost(_towerDefinition.BaseCost, wave,
                _towerDefinition.FlatModifier, _towerDefinition.PercentageModifier);
            _costTxt.text = Mathf.FloorToInt(_currentTowerCost).ToString();
        }
        
        public void OnUnitySelect()
        {
            _upgradeButton.SetActive(!_upgradeButton.activeSelf);
        }

        public void UpgradeUnity()
        {
            bool purchased = _walletManager.UpgradeTower(_currentTowerCost);
            if (!purchased)
            {
                _notifyUser.Invoke("No funds");
                return;
            }
            else
            {
                LevelUp();
            }
            _upgradeButton.SetActive(false);
        }

        private void UpdateLevelInfo()
        {
            _currentLevel++;
            _levelTxt.text = _currentLevel.ToString();
        }
        
        private void LevelUp()
        {
            UpdateLevelInfo();
            _onUpgradeTowerTrigger?.Invoke();
        }

        public void Cancel()
        {
            _upgradeButton.SetActive(false);
        }
        
    }
}