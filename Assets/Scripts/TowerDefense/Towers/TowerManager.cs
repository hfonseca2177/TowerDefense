using TowerDefense.Events;
using TowerDefense.Grid;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Tower Manager - responsible for tower placement
    /// </summary>
    public class TowerManager : MonoBehaviour
    {
        [Header("Tower Manager - responsible for tower placement")]
        [Tooltip("Placement Grid Manager")]
        [SerializeField] private GridManager _gridManager;
        [Tooltip("Tower prefab to be instantiated on placement")]
        [SerializeField] private GameObject _tower;
        [Tooltip("Player pointer input event")]
        [SerializeField] private PointerInputEventAsset _onTowerAcquired;
        [Tooltip("Enables debug trace")]
        [SerializeField] private bool _isDebugEnabled;

        private void OnEnable()
        {
            _onTowerAcquired.OnInvoked.AddListener(OnTowerAcquiredEvent);
        }

        private void OnDisable()
        {
            _onTowerAcquired.OnInvoked.RemoveListener(OnTowerAcquiredEvent);
        }

        private void OnTowerAcquiredEvent(Vector3 pointer)
        {
            if (pointer.magnitude <= float.Epsilon) return;
            Vector3 position = _gridManager.SnapToGrid(pointer);
            if (_gridManager.IsAlreadyAllocated(position)) return;
            PlaceTower(position);
            _gridManager.AllocateGrid(position);
            if(_isDebugEnabled) Debug.Log($"SnapTo: {position}");
        }
        
        private void PlaceTower(Vector3 position)
        {
            Instantiate(_tower, position, Quaternion.identity);
        }
        
         
    }
}
