using UnityEngine;

namespace TowerDefense.Events
{
    /// <summary>
    /// Serializable event that sends a game object as parameter
    /// </summary>
    [CreateAssetMenu(menuName = "TOWER_DEFENSE/Events/Game Object Event Asset")]
    public class GameObjectEventAsset: GameEventAsset<GameObject>
    {
    }
}