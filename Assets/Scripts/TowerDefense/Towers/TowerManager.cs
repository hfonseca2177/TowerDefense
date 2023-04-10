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
        [SerializeField, Tooltip("Placement Grid Manager")] private GridManager _gridManager;
        [SerializeField, Tooltip("Tower prefab to be instantiated on placement")] private GameObject _tower;
        [SerializeField, Tooltip("Player pointer input event")] private PointerInputEventAsset _onTowerAcquired;
        [SerializeField, Tooltip("Enables debug trace")] private bool _isDebugEnabled;

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
            Debug.Log($"SnapTo: {position}");
            PlaceTower(position);
        }
        
        private void PlaceTower(Vector3 position)
        {
            Instantiate(_tower, position, Quaternion.identity);
        }
        
         
    }
}
