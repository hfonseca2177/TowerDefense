using UnityEngine;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Shares current enemy scalable stats
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyLevelReference", menuName = "TOWER_DEFENSE/Enemy/Level Reference")]
    public class EnemyLevelReference : ScriptableObject
    {
        public EnemyStatsDTO Damage;
        public EnemyStatsDTO HitPoints;
        public EnemyStatsDTO Speed;
    }
}