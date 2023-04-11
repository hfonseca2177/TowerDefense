using UnityEngine;

namespace TowerDefense.Enemies
{
    [CreateAssetMenu(fileName = "_EnemyDefinition", menuName = "TOWER_DEFENSE/Enemy/Enemy Definition", order = 1)]
    public class EnemyDefinition : ScriptableObject
    {
        public int BaselineHitPoints = 1;
        public float BaselineSpeed = 3;
        public int BaseLineDamage = 1;
    }
}