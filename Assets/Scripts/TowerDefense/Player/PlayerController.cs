using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Player
{
    /// <summary>
    /// Game player Controller
    /// </summary>
    public class PlayerController : InputController
    {
        [SerializeField, Tooltip("Player pointer input event")] private PointerInputEventAsset _onTowerAcquiredNotify;
        
        public override void OnPointerDown(Vector3 worldPosition)
        {
            base.OnPointerDown(worldPosition);
            _onTowerAcquiredNotify.Invoke(worldPosition);
        }
    }
}
