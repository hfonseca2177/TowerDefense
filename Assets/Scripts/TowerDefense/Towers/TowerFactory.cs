using System.Linq;
using TowerDefense.Events;
using TowerDefense.Game;
using TowerDefense.Grid;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Place a tower based on tower Id purchased
    /// </summary>
    public class TowerFactory : MonoBehaviour
    {
        [Header("Tower Manager - responsible for tower placement")]
        [Tooltip("Placement Grid Manager")]
        [SerializeField] private GridManager _gridManager;
        [Tooltip("Currency manager needed for purchase transactions")]
        [SerializeField] private WalletManager _walletManager;
        [Tooltip("Tower Definitions list")]
        [SerializeField] private TowerDefinition[] _towerDefinitions;
        [Header("Events")]
        [Tooltip("Notifies that Tower was purchased")]
        [SerializeField] private TowerPurchaseEventAsset _onTowerPurchase;
        [Tooltip("Whenever Tower placement fails notifies purchase canceled")]
        [SerializeField] private TowerPurchaseEventAsset _onTowerPurchaseCanceledNotify;
        [Tooltip("Pops a message to user")]
        [SerializeField] private StringEventAsset _notifyUser;
        [Tooltip("Enables debug trace")]
        [SerializeField] private bool _isDebugEnabled;
        
        private void OnEnable()
        {
            _onTowerPurchase.OnInvoked.AddListener(OnTowerPurchasedEvent);
        }

        private void OnDisable()
        {
            _onTowerPurchase.OnInvoked.RemoveListener(OnTowerPurchasedEvent);
        }


        private void OnTowerPurchasedEvent(TowerPurchaseDTO towerPurchase)
        {
            PlaceTower(towerPurchase);
        }

        private void PlaceTower(TowerPurchaseDTO towerPurchase)
        {
            TowerDefinition towerDefinition = _towerDefinitions.FirstOrDefault(t => t.Id == towerPurchase.Id);
            if (towerDefinition == null)
            {
                CancelPlacement(towerPurchase);
                return;
            }
            if (towerPurchase.Position.magnitude <= float.Epsilon) return;
            Vector3 snappedPosition = _gridManager.SnapToGrid(towerPurchase.Position);
            if (_gridManager.IsAlreadyAllocated(snappedPosition)) return;
            float towerCost = TowerCostHelper.Instance.GetPurchaseCost(towerDefinition.BaseCost, WaveListener.Instance.WaveIndex,
                towerDefinition.FlatModifier, towerDefinition.PercentageModifier);
            bool purchased = _walletManager.PurchaseTower(towerCost);
            if (!purchased)
            {
                _notifyUser.Invoke("No funds");
                return;
            }
            _gridManager.AllocateGrid(snappedPosition);
            Instantiate(towerDefinition.TowerPrefab, snappedPosition, Quaternion.identity);
            if(_isDebugEnabled) Debug.Log($"SnapTo: {snappedPosition}");
        }

        private void CancelPlacement(TowerPurchaseDTO towerPurchase)
        {
            _onTowerPurchaseCanceledNotify.Invoke(towerPurchase);
        }
        
        
    }
}