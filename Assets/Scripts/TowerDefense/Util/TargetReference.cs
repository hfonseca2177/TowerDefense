using UnityEngine;

namespace TowerDefense.Util
{
    /// <summary>
    /// Serialized reference to the global target for any enemy which is the game base goal line
    /// </summary>
    [CreateAssetMenu(fileName = "_TargetReference", menuName = "TOWER_DEFENSE/Util/Target Reference", order = 0)]
    public class TargetReference : ScriptableObject
    {
        public Transform Target;
    }
}