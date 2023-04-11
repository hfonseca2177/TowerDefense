using System;
using TowerDefense.Events;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Towers
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
        private Text _label;
        private Button _button;
        

        private void Awake()
        {
            InitButton();
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