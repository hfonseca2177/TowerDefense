using TowerDefense.Events;
using TowerDefense.Grid;
using UnityEngine;

namespace TowerDefense.Player
{
    /// <summary>
    /// Game player Controller
    /// </summary>
    public class PlayerController : InputController
    {
        [Header("PLayer controller - player input actions")]
        [Tooltip("Whenever Player selects a tower for placement")]
        [SerializeField] private PointerInputEventAsset _onTowerAcquiredNotify;
        [Tooltip("Placement Grid Manager")]
        [SerializeField] private GridManager _gridManager;
       
        private bool _isPlacementActive;

        //button placement cancel selection 
        public void OnCancelPlacementEvent()
        {
            _isPlacementActive = false;
            _gridManager.DisableHighlightSpot();
        }

        //button placement selection
        public void OnSelectForPlacementEvent()
        {
            _isPlacementActive = true;
        }

        public override void OnPointerDown(Vector3 worldPosition)
        {
            base.OnPointerDown(worldPosition);
            if (_isPlacementActive)
            {
                _isPlacementActive = false;
                _onTowerAcquiredNotify.Invoke(worldPosition);
            }
            else
            {
                GridCell cell = _gridManager.PositionToGrid(worldPosition);
                if (cell.WorldPosition.magnitude > 0)
                {
                    Debug.Log("CELL FOUND");
                }
            }
        }

        private void LateUpdate()
        {
            if (!_isPlacementActive) return;
            var pointer = GetPointerPosition();
            _gridManager.HighLightSpotGridCell(pointer);
        }
    }
}
