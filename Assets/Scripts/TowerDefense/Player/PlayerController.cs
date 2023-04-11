using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Player
{
    /// <summary>
    /// Game player Controller
    /// </summary>
    public class PlayerController : InputController
    {
        [Tooltip("Enable tower purchase and placement")]
        [SerializeField] private TowerPurchaseEventAsset _onTowerPurchaseSelect;
        [Tooltip("Notifies that Tower was purchased")]
        [SerializeField] private TowerPurchaseEventAsset _onTowerPurchaseNotify;
        [Tooltip("Whenever Tower placement fails return the purchase back")]
        [SerializeField] private TowerPurchaseEventAsset _onTowerPurchaseCanceled;
        
        
        private bool _isPlacementActive;
        private TowerPurchaseDTO _currentTowerPurchase;
        
        private void OnEnable()
        {
            _onTowerPurchaseSelect.OnInvoked.AddListener(OnTowerPurchaseSelectEvent);
            _onTowerPurchaseCanceled.OnInvoked.AddListener(OnTowerPurchaseCanceledEvent);
        }

        private void OnDisable()
        {
            _onTowerPurchaseSelect.OnInvoked.RemoveListener(OnTowerPurchaseSelectEvent);
            _onTowerPurchaseCanceled.OnInvoked.RemoveListener(OnTowerPurchaseCanceledEvent);
        }

        private void OnTowerPurchaseCanceledEvent(TowerPurchaseDTO towerPurchase)
        {
            RefundPurchase(towerPurchase);
        }

        private void OnTowerPurchaseSelectEvent(TowerPurchaseDTO towerPurchase)
        {
            OnSelectForPlacement(towerPurchase);
        }

        //placement cancel selection triggered by UI 
        public void OnCancelPlacementEvent()
        {
            _isPlacementActive = false;
        }

        private void OnSelectForPlacement(TowerPurchaseDTO towerPurchase)
        {
            _isPlacementActive = true;
            _currentTowerPurchase = towerPurchase;
        }

        public override void OnPointerDown(Vector3 worldPosition)
        {
            base.OnPointerDown(worldPosition);
            if (_isPlacementActive)
            {
                _isPlacementActive = false;
                _currentTowerPurchase.Position = worldPosition;
                _onTowerPurchaseNotify.Invoke(_currentTowerPurchase);
            }
        }

        private void RefundPurchase(TowerPurchaseDTO towerPurchase)
        {
            //TODO refund purchase cost to wallet
        }
    }
}
