using UnityEngine;

namespace TowerDefense.Events
{
    /// <summary>
    /// Serializable event that sends a float value
    /// </summary>
    [CreateAssetMenu(menuName = "TOWER_DEFENSE/Events/Float Event Asset")]
    public class FloatEventAsset: GameEventAsset<float>
    {
    }
}