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
        public AttributeDefinition[] Attributes;

    }
}