using UnityEngine;

namespace TowerDefense.Enemies
{
    /// <summary>
    /// Helper class to Separate core calculation update logic from the system
    /// </summary>
    public class EnemyScaleHelper
    {
        private static EnemyScaleHelper _instance;
        public static EnemyScaleHelper Instance
        {
            get { return _instance ??= new EnemyScaleHelper(); }
        }
        
        private EnemyScaleHelper()
        {
        }
        
        public float GetUpgradedValue(float baseCost, int wave, float flatModifier, float percentageModifier)
        {
            return (baseCost * Mathf.Pow((1 + percentageModifier),wave)) + (flatModifier * wave);
        }
        
    }
}