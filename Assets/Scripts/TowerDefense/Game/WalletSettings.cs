using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Currency settings
    /// </summary>
    [CreateAssetMenu(fileName = "WalletSettings", menuName = "TOWER_DEFENSE/Game/Wallet Settings", order = 1)]
    public class WalletSettings : ScriptableObject
    {
        public string CurrencyName;
        public Sprite Icon;
        public float InitialBudget;
        public float ConversionRate;

    }
}