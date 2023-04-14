using UnityEngine;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Serialized reference to the global target for any enemy which is the game base goal line
    /// </summary>
    [CreateAssetMenu(fileName = "_TargetReference", menuName = "TOWER_DEFENSE/Enemy/Target Reference", order = 0)]
    public class TargetReference : ScriptableObject
    {
        public Transform Target;
    }
}