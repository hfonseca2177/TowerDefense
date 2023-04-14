using TMPro;
using TowerDefense.Events;
using TowerDefense.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    /// <summary>
    /// Tower Purchase UI Button - starts tower placement process
    /// </summary>
    public class TowerPurchaseButton : MonoBehaviour
    {
        [Tooltip("Type of tower definition")]
        [SerializeField] private TowerDefinition _towerDefinition;
        [Tooltip("Enable tower purchase and placement")]
        [SerializeField] private TowerPurchaseEventAsset _onTowerPurchaseSelectNotify;
        [Tooltip("whenever a new Wave starts")]
        [SerializeField] private IntEventAsset _onNewWave;
        [SerializeField] private TextMeshProUGUI _costTxt;
        private Text _label;
        private Button _button;
        
        private void Awake()
        {
            InitButton();
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
            float towerCost = TowerCostHelper.Instance.GetPurchaseCost(_towerDefinition.BaseCost, wave,
                _towerDefinition.FlatModifier, _towerDefinition.PercentageModifier);
            _costTxt.text = Mathf.FloorToInt(towerCost).ToString();
            
        }

        private void InitButton()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SubmitTowerPurchaseSelect);
            var buttonColors = _button.colors;
            buttonColors.normalColor = _towerDefinition.ThemeColor;
            _button.colors = buttonColors;
            _label = GetComponentInChildren<Text>();
            _label.text = _towerDefinition.Name;
            UpdateCost(0);
        }
        
        

        private void SubmitTowerPurchaseSelect()
        {
            TowerPurchaseDTO towerPurchase = new TowerPurchaseDTO
            {
                Id = _towerDefinition.Id
            };
            _onTowerPurchaseSelectNotify.Invoke(towerPurchase);
        }
    }
}