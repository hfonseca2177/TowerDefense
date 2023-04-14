using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Tower Base Attributes that can be upgraded
    /// </summary>
    [CreateAssetMenu(fileName = "_Attribute", menuName = "TOWER_DEFENSE/Towers/Attribute Definition Asset", order = 1)]
    public class AttributeDefinition: ScriptableObject
    {
        public string StatName;
        public float BaseLine;
        public bool HasCap;
        public float CapValue;
        public bool IsTrait;
        public int UnlockLevel;
        public float FlatModifier;
        public float PercentageModifier;
        public Sprite Icon;

    }
}