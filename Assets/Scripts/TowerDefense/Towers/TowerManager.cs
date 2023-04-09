using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Tower Manager - responsible for tower placement
    /// </summary>
    public class TowerManager : MonoBehaviour
    {
        [Header("Tower Manager - responsible for tower placement")]
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
            PlaceTower(pointer);
        }
        
        private void PlaceTower(Vector3 position)
        {
            Instantiate(_tower, position, Quaternion.identity);
        }
        
         
    }
}
