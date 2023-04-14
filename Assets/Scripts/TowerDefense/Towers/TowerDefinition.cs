using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Tower Attributes definition
    /// </summary>
    [CreateAssetMenu(fileName = "_tower", menuName = "TOWER_DEFENSE/Towers/Tower Definition Asset", order = 0)]
    public class TowerDefinition : ScriptableObject
    {
        public int Id;
        public string Name;
        public Color ThemeColor;
        public GameObject TowerPrefab;
        [Expandable] public AttributeDefinition Damage;
        [Expandable] public AttributeDefinition Speed;
        [Expandable] public AttributeDefinition Range;
        [Expandable] public AttributeDefinition Special;
        [Expandable] public AttributeDefinition[] OtherAttributes;
        public float BaseCost;
        public float FlatModifier;
        public float PercentageModifier;

    }
}