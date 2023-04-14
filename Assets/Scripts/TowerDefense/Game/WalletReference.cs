using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Creates a serialized reference of game object for dynamic ones that cant have it injected
    /// </summary>
    [CreateAssetMenu(fileName = "WalletReference", menuName = "TOWER_DEFENSE/Game/Wallet Reference", order = 0)]
    public class WalletReference : ScriptableObject
    {
        public WalletManager Wallet;
    }
}