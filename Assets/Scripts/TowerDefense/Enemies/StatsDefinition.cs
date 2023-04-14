using UnityEngine;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Enemy Scale attributes - naming it as stats instead of attributes to easier understanding
    /// and not confusing with tower attributes. Works same principle of growth
    /// </summary>
    [CreateAssetMenu(fileName = "_Stats", menuName = "TOWER_DEFENSE/Enemy/Stats Definition Asset", order = 0)]
    public class StatsDefinition : ScriptableObject
    {
        public string StatName;
        public float BaseLine;
        public bool HasCap;
        public float CapValue;
        public float FlatModifier;
        public float PercentageModifier;
        
    }
}