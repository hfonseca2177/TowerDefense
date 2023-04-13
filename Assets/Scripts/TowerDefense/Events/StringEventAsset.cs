using UnityEngine;

namespace TowerDefense.Events
{
    /// <summary>
    /// Serializable event that sends a string value
    /// </summary>
    [CreateAssetMenu(menuName = "TOWER_DEFENSE/Events/String Event Asset")]
    public class StringEventAsset : GameEventAsset<string>
    {
    }
}