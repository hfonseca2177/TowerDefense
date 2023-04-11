using UnityEngine;

namespace TowerDefense.Events
{
    /// <summary>
    /// Serializable event that sends a position value
    /// </summary>
    [CreateAssetMenu(menuName = "TOWER_DEFENSE/Events/Tower Purchase Event Asset")]
    public class TowerPurchaseEventAsset: GameEventAsset<TowerPurchaseDTO>
    {
        
    }
}