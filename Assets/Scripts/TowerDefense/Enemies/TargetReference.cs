using UnityEngine;

namespace TowerDefense.Enemies
{
    [CreateAssetMenu(fileName = "_TargetReference", menuName = "TOWER_DEFENSE/Enemy/Target Reference", order = 0)]
    public class TargetReference : ScriptableObject
    {
        public Transform Target;
    }
}