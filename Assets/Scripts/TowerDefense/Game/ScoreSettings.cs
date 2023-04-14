using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Custom Wave settings for a specific wave.
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreSettings", menuName = "TOWER_DEFENSE/Game/Score Settings", order = 0)]
    public class ScoreSettings : ScriptableObject
    {
        //Base calculation value
        public float BaseValue;
        //Percentage modifier to be applied
        public float PercentageModifier;
        //Flat value that can be added
        public float FlatModifier;
        //Frequency of score
        public float Rate;
    }
}