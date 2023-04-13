using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Enemy stats and behavior definition
    /// </summary>
    [CreateAssetMenu(fileName = "_EnemyDefinition", menuName = "TOWER_DEFENSE/Enemy/Enemy Definition", order = 1)]
    public class EnemyDefinition : ScriptableObject
    {
        public int BaselineHitPoint;
        public float BaselineSpeed;
        public int BaseLineDamage;
        public float SpawnCost;
        public float Score;
        public VoidEventAsset SpawnRequestTrigger;
    }
}