using System;

namespace TowerDefense.Enemies
{
    [Serializable]
    public class EnemyStatsDTO
    {
        public readonly StatsDefinition Definition;
        public float CurrentValue;
        private bool _capReached;

        public EnemyStatsDTO()
        {
        }
        
        public EnemyStatsDTO(StatsDefinition statsDefinition)
        {
            Definition = statsDefinition;
            CurrentValue = Definition.BaseLine;
        }

        public void ScaleUp(int wave)
        {
            if (_capReached) return;
            CurrentValue = EnemyScaleHelper.Instance.GetUpgradedValue(Definition.BaseLine, wave, Definition.FlatModifier,
                Definition.PercentageModifier);
            if (Definition.HasCap && CurrentValue > Definition.CapValue)
            {
                CurrentValue = Definition.CapValue;
                _capReached = true;
            }
        }
    }
}