namespace TowerDefense.Towers
{
    /// <summary>
    /// Manages individual attribute level up 
    /// </summary>
    public class TowerAttributesDTO
    {
        public readonly AttributeDefinition Definition;
        public float CurrentValue;
        public int Level;
        private bool _capReached;
        public bool IsUnlocked
        {
            get {return CanUnlock(Level);}
        }

        public TowerAttributesDTO(AttributeDefinition attributeDefinition)
        {
            Definition = attributeDefinition;
            CurrentValue = Definition.BaseLine;
        }

        private bool CanUnlock(int level)
        {
            return level > Definition.UnlockLevel;
        }

        public void LevelUp()
        {
            if (_capReached) return;
            if(Definition.IsTrait && !CanUnlock(Level + 1)) return;
            CurrentValue = TowerUpdateHelper.Instance.GetUpgradedValue(Definition.BaseLine, Level + 1, Definition.FlatModifier,
                Definition.PercentageModifier);
            if (Definition.HasCap && CurrentValue > Definition.CapValue)
            {
                CurrentValue = Definition.CapValue;
                _capReached = true;
            }
            //case unlocked and not on cap
            Level++;
        }
    }
}