using UnityEngine;

namespace TowerDefense.Player
{
    [CreateAssetMenu(fileName = "_PlayerBaseStats", menuName = "TOWER_DEFENSE/Player/Base Stats", order = 0)]
    public class PlayerBaseStats : ScriptableObject
    {
        public int HitPoints;
    }
}