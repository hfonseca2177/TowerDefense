using UnityEngine;

namespace TowerDefense.Events
{
    /// <summary>
    /// Serializable event that sends a position value
    /// </summary>
    [CreateAssetMenu(menuName = "TOWER_DEFENSE/Events/Pointer Input Event Asset")]
    public class PointerInputEventAsset: GameEventAsset<Vector3>
    {
    }
}