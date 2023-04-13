using UnityEngine;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Custom Wave settings for a specific wave.
    /// </summary>
    [CreateAssetMenu(fileName = "WaveSettings", menuName = "TOWER_DEFENSE/Enemy/Wave Settings", order = 0)]
    public class WaveSettings : ScriptableObject
    {
        //wave index
        public int Wave;
        //Minimum budget cost to be sorted
        public float MinimumCost;
        //Dump modifier for the Cost Budget
        public float DumpFactor;
        //interval between waves
        public float WavesInterval;
        //interval between waves
        public float SpawnInterval;
        //enemy pool for the wave
        public EnemyDefinition[] Enemies;
    }
}