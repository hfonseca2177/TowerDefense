using UnityEngine;

namespace TowerDefense.Events
{
    /// <summary>
    /// Serializable event that sends a int value
    /// </summary>
    [CreateAssetMenu(menuName = "TOWER_DEFENSE/Events/Int Event Asset")]
    public class IntEventAsset: GameEventAsset<int>
    {
    }
}